using System.Collections.Generic;


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
        ID  //直接対応するトークンなし
    };

    readonly TokenDictionary mapFromWordToTokenType = new(){
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

    List<string> convertToKeysList(TokenDictionary.ValueCollection collection)
    {
        var list = new List<string>();
        foreach (string key in collection) list.Add(key);
        return list;
    }

    List<Token> Lex(string code) {
        var tokens = new List<Token>();

        List<string> AllTokenCandidates = convertToKeysList(mapFromWordToTokenType.Values);
        List<string> tokenCandidates = convertToKeysList(mapFromWordToTokenType.Values);        
        string chainTowardToken = "";

        for (int textPointer = 0; textPointer < code.Length; textPointer++) {
            chainTowardToken += code[textPointer];
            foreach (string token in tokenCandidates)
            {
                if (!token.StartsWith(chainTowardToken)) tokenCandidates.Remove(token);
            }
            if (tokenCandidates.Count != 1) 

        }
        return tokens;
    }
    
}