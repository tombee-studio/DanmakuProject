using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ReservedTokenDictionary = System.Collections.Generic.Dictionary<EnemyLexer.TokenType, string>;
using VariableTokenDictionary = System.Collections.Generic.Dictionary<EnemyLexer.TokenType, System.Text.RegularExpressions.Regex>;
public class EnemyLexer {
    public enum TokenType
    {
        /**
         * Types of Token
         */
        NONE,   
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

        USER_DEFINED_SYMBOL,
        INT_LITERAL,
        FLOAT_LITERAL

    };

    readonly static ReservedTokenDictionary mapFromTokenTypeToReservedWord = new(){
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
            { TokenType.BRACKETS_LEFT, "(" },
            { TokenType.BRACKETS_RIGHT, ")" },
            { TokenType.ASSIGNMENT, "=" },
            { TokenType.COMMA, "," }
    };
    readonly static ReservedTokenDictionary.KeyCollection reservedTokenTypes = mapFromTokenTypeToReservedWord.Keys;

    readonly static VariableTokenDictionary mapFromTokenTypeToVariableWord = new()
    {
        { TokenType.USER_DEFINED_SYMBOL,    new Regex("^[a-zA-Z_][0-9a-zA-Z_]*$") },
        { TokenType.INT_LITERAL,            new Regex("^[0-9]+$") },
        { TokenType.FLOAT_LITERAL,          new Regex("^[0-9]+(\\.[0-9]+)?f$") }
    };
    /*
     *  
     */
    
    readonly static VariableTokenDictionary.KeyCollection variableTokenTypes = mapFromTokenTypeToVariableWord.Keys;

    public struct Token {
        public TokenType type;
        public string user_defined_symbol;
        public int int_val;
        public float float_val;

        public static Token GenerateReservedToken(TokenType giventype)
        {
            var token = new Token();
            token.type = giventype;
            return token;
        }
        public static Token GenerateUserDefinedSymbolToken(string given_user_defined_symbol)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.user_defined_symbol = given_user_defined_symbol;
            return token;
        }
        public static Token GenerateIntValToken(string given_int_val)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.int_val = int.Parse(given_int_val);
            return token;
        }
        public static Token GenerateFloatValToken(string given_float_val)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.float_val = float.Parse(given_float_val);
            return token;
        }
    };


    public List<Token> Lex(string code) {

        var tokens = new List<Token>();

        string snippet = "";

        for (int textPointer = 0; textPointer - 1 < code.Length; textPointer++) {
            snippet += code[textPointer];
            if (!isThereMatchedTokenWhenLookaheading(snippet, code[textPointer + 1]))
            {
                var matchedTokenType = FindOutMatchedTokenTypeInList(snippet);
                tokens.Add(generateToken(snippet, matchedTokenType));
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
            // 123f142gなど
            // こういうトークンの場合はどうする？
            Regex regexForVariableWords = GetValue(mapFromTokenTypeToVariableWord, tokenType);
            if (!regexForVariableWords.IsMatch(chainTowardToken)) pickedtokens.Add(tokenType);
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
        }
        foreach (TokenType tokenType in variableTokenTypes)
        {
            if (!GetValue(mapFromTokenTypeToVariableWord, tokenType).IsMatch(targetSnippet)) continue;
        }
        throw new Exception($"未知のトークン{targetSnippet}が見つかりました。");
    }
    /**
     * 一文字先読みした際に当てはまる可能性のあるトークンが存在するかを調べる。
     * 当てはまる可能性のあるトークンが存在しなかった場合、トークンは少なくともsnippet.length以下の長さであることがわかる。
     */
    bool isThereMatchedTokenWhenLookaheading(string snippet, char nextChar)
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
    /**
     * 与えられたトークンの情報から適切にトークンを生成する。
     */
    Token generateToken(string snippet, TokenType token)
    {
        switch (token)
        {
            case TokenType.USER_DEFINED_SYMBOL:
                return Token.GenerateUserDefinedSymbolToken(snippet);
            case TokenType.INT_LITERAL:
                return Token.GenerateIntValToken(snippet);
            case TokenType.FLOAT_LITERAL:
                return Token.GenerateIntValToken(snippet);
            default:
                return Token.GenerateReservedToken(token);
        }
    }


    
}