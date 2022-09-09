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
