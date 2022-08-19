using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using TokenDictionary = System.Collections.Generic.Dictionary<EnemyLexer.TokenType, string>;

public class EnemyLexer {
    public enum TokenType
    {
        /**
         * Types of Token
         */
        NONE,   //直接対応するトークンなし
        BEHAVIOR,
        BULLET,
        ACTION,
        PHASE_NAVIGATOR,
        ID_NAVIGATOR,
        INT,
        FLOAT,
        REPEAT,
        BREAK,
        INFINITY,
        IF,
        ELSE,
        PLUS,
        SUB,
        MULTIPLY,
        DIVIDE,
        MOD,
        AND,
        OR,
        NOT,
        GREATER_EQUAL,
        LESS_EQUAL,
        GREATER_THAN,
        LESS_THAN,
        BRACKETS_LEFT,
        BRACKETS_RIGHT,
        ASSIGNMENT,
        EQUAL,
        COMMA,
        ID
    };

    readonly TokenDictionary mapFromTokenTypeToReservedWord = new(){
            { TokenType.BEHAVIOR, "behavior" },
            { TokenType.BULLET, "bullet" },
            { TokenType.ACTION, "action" },
            { TokenType.PHASE_NAVIGATOR, ">>" },
            { TokenType.ID_NAVIGATOR, "ID" },
            { TokenType.INT, "int" },
            { TokenType.FLOAT, "float" },
            { TokenType.REPEAT, "repeat" },
            { TokenType.BREAK, "break" },
            { TokenType.INFINITY, "infinity" },
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
            { TokenType.BRACKETS_LEFT, "(" },
            { TokenType.BRACKETS_RIGHT, ")" },
            { TokenType.ASSIGNMENT, "=" },
            { TokenType.COMMA, "," }
    };

    struct Token {
        public TokenType type;
        public string id;
        public int int_val;
        public float float_val;
    };

    List<string> getReservedWords() => convertToList(mapFromTokenTypeToReservedWord.Values);

    List<T> convertToList<T>(IEnumerable<T> collection)
    {
        var list = new List<T>();
        foreach (T key in collection) list.Add(key);
        return list;
    }


    List<Token> Lex(string code) {
        var tokens = new List<Token>();

        List<string> tokenCandidates = getReservedWords();
        string chainTowardToken = "";

        for (int textPointer = 0; textPointer < code.Length; textPointer++) {
            chainTowardToken += code[textPointer];

            // 先頭からトークンを読み進めた際に、現地点であり得ないトークンの可能性を棄却する。
            foreach (TokenType tokenType in tokenCandidates)
            {
                if (!patternString.StartsWith(chain)) tokenCandidates.Remove(tokenType);
            }
            
            

        }
        return tokens;
    }

    bool IsChainInPatternOf(TokenType tokenType, string chain)
    {
        if (tokenType == TokenType.ID) return IsID(chain);

        string patternString;
        if (mapFromTokenTypeToReservedWord.TryGetValue(tokenType, out patternString)) throw new Exception($"TokenType {tokenType} is not found in mapFromWordToTokenType.Keys.");
        return ;
    }

}