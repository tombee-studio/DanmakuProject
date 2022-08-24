public partial class EnemyLexer
{
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



}