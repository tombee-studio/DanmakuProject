using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;

using TokenType = ScriptToken.Type;

using ReservedTokenDictionary = System.Collections.Generic.Dictionary<ScriptToken.Type, string>;
using VariableTokenDictionary = System.Collections.Generic.Dictionary<ScriptToken.Type, System.Func<string, EnemyLexer.MatchResult>>;
using UnityEngine;

public partial class EnemyLexer {
    private readonly char[] skippedCharacters = { ' ', '\n', '\t' };

    public class InvalidTokenException : Exception
    {
        readonly int lineNumber;
        public InvalidTokenException(string message, string errorExplanation):base(message + $"\n\nErrorExplanation :\n {errorExplanation}"){}
    }
    public enum MatchResult
    {
        Match,
        PartialMatch,
        NoMatch
    }
    public readonly static ReservedTokenDictionary mapFromTokenTypeToReservedWord = new()
    {
        { TokenType.BEHAVIOR, "behavior" },

        { TokenType.BULLET, "bullet" },
        { TokenType.ACTION, "action" },
        { TokenType.PHASE_SEPARATOR, ">>" },

        { TokenType.ID_NAVIGATOR, "ID" },
        { TokenType.COLON, ":" },

        { TokenType.INT, "int" },
        { TokenType.FLOAT, "float" },
        { TokenType.REPEAT, "repeat" },
        { TokenType.BREAK, "break" },
        { TokenType.INFINITY, "Infinity" },
        { TokenType.IF, "if" },
        { TokenType.ELSE, "else" },
        { TokenType.AND, "and" },
        { TokenType.OR, "or" },
        { TokenType.NOT, "not" },
        { TokenType.GREATER_EQUAL, ">=" },
        { TokenType.LESS_EQUAL, "<=" },
        { TokenType.EQUAL, "==" },
        { TokenType.NOT_EQUAL, "!=" },
        { TokenType.PLUS, "+" },
        { TokenType.SUB, "-" },
        { TokenType.MULTIPLY, "*" },
        { TokenType.DIVIDE, "/" },
        { TokenType.MOD, "%" },
        { TokenType.GREATER_THAN, ">" },
        { TokenType.LESS_THAN, "<" },
        { TokenType.BRACKET_LEFT, "(" },
        { TokenType.BRACKET_RIGHT, ")" },

        { TokenType.CURLY_BRACKET_LEFT, "{" },
        { TokenType.CURLY_BRACKET_RIGHT, "}" },

        { TokenType.ASSIGNMENT, "=" },
        { TokenType.COMMA, "," }
    };
    public readonly static ReservedTokenDictionary.KeyCollection reservedTokenTypes = mapFromTokenTypeToReservedWord.Keys;

    public readonly static VariableTokenDictionary mapFromTokenTypeToVariableWord = new()
    {
        { TokenType.SYMBOL_ID,
            (str) =>
                Regex.IsMatch(str, "^[a-zA-Z_][0-9a-zA-Z_]*$")
                ? MatchResult.Match : MatchResult.NoMatch
        },
        {
            TokenType.FLOAT_LITERAL,
            (str) =>
            {
                bool partialMatchResult = Regex.IsMatch(str, "^[0-9]+(\\.[0-9]*)?$");
                if (!partialMatchResult)
                {
                    return
                        (Regex.IsMatch(str, "^[0-9]+(\\.[0-9]+)?f$"))
                        ? MatchResult.Match : MatchResult.NoMatch;
                }

                return MatchResult.PartialMatch;
            }
        },
        { TokenType.INT_LITERAL,
            (str) =>
                Regex.IsMatch(str, "^[0-9]+$")
                ? MatchResult.Match : MatchResult.NoMatch
        }
    };

    public readonly static VariableTokenDictionary.KeyCollection variableTokenTypes = mapFromTokenTypeToVariableWord.Keys;


    public List<ScriptToken> Lex(string code) {
        if (code.Length == 0)
        {
            List<ScriptToken> emptyList = new List<ScriptToken>();
            return emptyList;
        }

        var lineCount = 0;
        var codeCharNumber = code.Length;
        
        var tokens = new List<ScriptToken>();
        string snippet = "";

        for (int textPointer = 0; textPointer + 1 < codeCharNumber; textPointer++) {

            char currentChar = code[textPointer];
            char nextChar = code[textPointer + 1];
            if (currentChar == '\n') lineCount++;
            
            if (shouldSkippedCurrentCharacter(snippet, currentChar)) continue;
            snippet += currentChar;
            if (!existPossibleTokenWhenLookaheading(snippet, nextChar)){
                tokens.Add(ConvertCurrentSnippetToToken(snippet, lineCount, code));
                snippet = "";
            }
        }

        if (shouldSkippedCurrentCharacter(snippet, code[codeCharNumber - 1])) return tokens;
        snippet += code[codeCharNumber - 1];
        tokens.Add(ConvertCurrentSnippetToToken(snippet, lineCount, code));
        return tokens;
    }
    /**
     * 現在の文字列を読み飛ばすべきか判定する
     */
    bool shouldSkippedCurrentCharacter(string currentSnippet, char currentCharacter)
    {
        
        return
            currentSnippet.Length == 0 &&
            Util_Array.x_in_collection(currentCharacter, skippedCharacters);
    }
    /**
     * snippetに含まれる文字列をトークン列に変換する
     */
    ScriptToken ConvertCurrentSnippetToToken(string snippet, int lineCount, string allCode)
    {
        var matchedTokenTypeInLastChar = FindOutMatchedTokenTypeInList(snippet, lineCount, allCode);
        return ScriptToken.GenerateToken(snippet, matchedTokenTypeInLastChar);
    }

    /*
     *  先頭からトークンを読み進めた際に、その地点で当てはまる可能性のある予約語を列挙する。
     *  結果として、新たなリストを返す。
     */   
    List<TokenType> PickupPossibleReservedWords(string chainTowardToken)
    {
        var pickedtokens = new List<TokenType>();
        foreach (TokenType tokenType in reservedTokenTypes)
        {
            string reservedWord = GetValue(mapFromTokenTypeToReservedWord, tokenType);
            if (reservedWord.StartsWith(chainTowardToken)) pickedtokens.Add(tokenType);
        }
        return pickedtokens;
    }
    /*
     *  先頭からトークンを読み進めた際に、その地点で当てはまる可能性のある可変語の種類を列挙する。
     *  結果として、新たなリストを返す。
     */
    List<TokenType> PickupPossibleVariableWords(string chainTowardToken)
    {
        var pickedtokens = new List<TokenType>();
        foreach (TokenType tokenType in variableTokenTypes)
        {
            var matchFunction = GetValue(mapFromTokenTypeToVariableWord, tokenType);
            var matchResult = matchFunction.Invoke(chainTowardToken);
            if (
                matchResult == MatchResult.PartialMatch ||
                matchResult == MatchResult.Match
            ) pickedtokens.Add(tokenType);
        }
        return pickedtokens;
    }
    /**
     *  与えられた予約語トークンおよび可変語トークンの中で、一致するトークンの候補を1つ見つけ出す。
     *  ただし、可変語と予約語のいずれでもあった場合、予約語として解釈する。
     *  発見できなかった場合、エラーを放出する。
     */
    TokenType FindOutMatchedTokenTypeInList(string targetSnippet, int lineCount, string allCode)
    {
        foreach (TokenType tokenType in reservedTokenTypes)
        {
            if (targetSnippet != GetValue(mapFromTokenTypeToReservedWord, tokenType)) continue;
            return tokenType;
        }
        foreach (TokenType tokenType in variableTokenTypes)
        {
            var matchFunction = GetValue(mapFromTokenTypeToVariableWord, tokenType);
            if (matchFunction.Invoke(targetSnippet) != MatchResult.Match) continue;
            return tokenType;
        }
        throw new InvalidTokenException(
            $"Unknown token \"{targetSnippet}\" is found",
            generateLexerErrorExplanation(allCode, lineCount)
        );
    }

    /**
     * 一文字先読みした際に当てはまる可能性のあるトークンが存在するかを調べる。
     * 当てはまる可能性のあるトークンが存在しなかった場合、トークンは少なくともsnippet.length以下の長さであることがわかる。
     */
    bool existPossibleTokenWhenLookaheading(string snippet, char nextChar)
    {
        if (Util_Array.x_in_collection(nextChar, skippedCharacters)) return false; 
        var snippetInNextChar = snippet + nextChar;
        return PickupPossibleReservedWords(snippetInNextChar).Count != 0
            || PickupPossibleVariableWords(snippetInNextChar).Count != 0;
    }

    S GetValue<T,S>(Dictionary<T,S> map, T key)
    {
        if (!map.TryGetValue(key, out S value))
            throw new Exception($"Key {key} is not found in mapFromTokenTypeToVariableWord.");
        return value;
    }
    
    private string generateLexerErrorExplanation(string allCode, int lineNumber)
    {
        var lines = allCode.Split("\n");
        const int delta = 3;
        string errorMsg = "";
        for (
            int lineIndex = Math.Max(0, lineNumber - delta);
            lineIndex <= Math.Min(lines.Length - 1, lineNumber + delta);
            lineIndex++
        ){
            errorMsg += $"line {lineIndex} >> {lines[lineIndex]}";
            if (lineIndex == lineNumber) errorMsg += " :: here!";
            errorMsg += "\n";
        }

        return errorMsg;
    }

    
}