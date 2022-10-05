using System;
public partial class EnemyVM
{
    public enum Mnemonic
    {
        PUSH,
        ADD,
        SUB,
        MUL,
        DIV,
        MOD,

        JMP,
        JE,
        JNE,
        BREAK,

        AND,
        OR,
        NOT,

        LT,
        LE,
        GT,
        GE,
        EQ,
        NE,

        CALL,

        LOAD,
        STORE
    };
}
