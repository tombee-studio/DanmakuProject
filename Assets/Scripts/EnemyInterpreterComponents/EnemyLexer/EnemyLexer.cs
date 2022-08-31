using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;

using TokenType = ScriptToken.Type;

using ReservedTokenDictionary = System.Collections.Generic.Dictionary<ScriptToken.Type, string>;
using VariableTokenDictionary = System.Collections.Generic.Dictionary<ScriptToken.Type, System.Func<string, EnemyLexer.MatchResult>>;

public partial class EnemyLexer {
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
        { TokenType.PHASE_NAVIGATOR, ">>" },
        { TokenType.ID_NAVIGATOR, "ID" },
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
        { TokenType.PLUS, "+" },
        { TokenType.SUB, "-" },
        { TokenType.MULTIPLY, "*" },
        { TokenType.DIVIDE, "/" },
        { TokenType.GREATER_THAN, ">" },
        { TokenType.LESS_THAN, "<" },
        { TokenType.BRACKET_LEFT, "(" },
        { TokenType.BRACKET_RIGHT, ")" },
        { TokenType.ASSIGNMENT, "=" },
        { TokenType.COMMA, "," }
    };
    public readonly static ReservedTokenDictionary.KeyCollection reservedTokenTypes = mapFromTokenTypeToReservedWord.Keys;

    public readonly static VariableTokenDictionary mapFromTokenTypeToVariableWord = new()
    {
        { TokenType.USER_DEFINED_SYMBOL,
            (str) => 
                new Regex("^[a-zA-Z_][0-9a-zA-Z_]*$").IsMatch(str)
                ? MatchResult.Match : MatchResult.NoMatch
        },
        { TokenType.INT_LITERAL,
            (str) =>
                new Regex("[0-9]+").IsMatch(str)
                ? MatchResult.Match : MatchResult.NoMatch},
        { TokenType.FLOAT_LITERAL,
            (str) =>
            {
                bool partialMatchResult = new Regex("[0-9]+(\\.[0-9]+)?").IsMatch(str);
                if (!partialMatchResult) { return MatchResult.NoMatch; }
                if (str.EndsWith("f")) { return MatchResult.Match; }
                else { return MatchResult.PartialMatch; }
            }
        }
    };

    public readonly static VariableTokenDictionary.KeyCollection variableTokenTypes = mapFromTokenTypeToVariableWord.Keys;


    public List<ScriptToken> Lex(string code) {

        var tokens = new List<ScriptToken>();

        string snippet = "";

        for (int textPointer = 0; textPointer < code.Length; textPointer++) {
            snippet += code[textPointer];
            if (textPointer == code.Length - 1 || !existPossibleTokenWhenLookaheading(snippet, code[textPointer + 1]))
            {
                var matchedTokenType = FindOutMatchedTokenTypeInList(snippet);
                tokens.Add(ScriptToken.GenerateToken(snippet, matchedTokenType));
                snippet = "";
            }

        }
        return tokens;
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
            // ただ単に正規表現で部分文字列であることは判定できない
            // FLOAT_VALの最後のfなど
            // 1234.143fなど
            // こういうトークンの場合はどうする？
            // 12
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
    TokenType FindOutMatchedTokenTypeInList(string targetSnippet)
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
        throw new Exception($"未知のトークン{targetSnippet}が見つかりました。");
    }
    /**
     * 一文字先読みした際に当てはまる可能性のあるトークンが存在するかを調べる。
     * 当てはまる可能性のあるトークンが存在しなかった場合、トークンは少なくともsnippet.length以下の長さであることがわかる。
     */
    bool existPossibleTokenWhenLookaheading(string snippet, char nextChar)
    {
        var snippetInNextChar = snippet + nextChar;
        return PickupPossibleReservedWords(snippetInNextChar).Count != 0
            || PickupPossibleVariableWords(snippetInNextChar).Count != 0;
    }

    S GetValue<T,S>(Dictionary<T,S> map, T key)
    {
        S value;
        if (!map.TryGetValue(key, out value))
            throw new Exception($"Key {key} is not found in mapFromTokenTypeToVariableWord.");
        return value;
    }
    


    
}