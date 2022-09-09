using System;
using System.Collections.Generic;

public class NumberASTNode : ASTNode
{
    class PrimitiveValueError : Exception
    {
        public PrimitiveValueError(string message) : base(message) { }
    }
    PrimitiveValue number;
    // 読み出しトークンの型に応じて呼び出しコンストラクタを変えられるようにしておく
    public NumberASTNode(PrimitiveValue value) { number = value; }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, number));
        return instructions;
    }

    public override string Print(int tab)
    {
        switch (number.type)
        {
            case PrimitiveValue.Type.INT:
                return GetTabs(tab) + $"{(int)number}";
            case PrimitiveValue.Type.FLOAT:
                return GetTabs(tab) + $"{(float)number}";
            default:
                throw new PrimitiveValueError($"Illegal type {number.type} received.");
        }
    }
}
