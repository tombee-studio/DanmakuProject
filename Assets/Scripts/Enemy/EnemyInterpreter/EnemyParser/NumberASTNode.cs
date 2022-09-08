using System;
using System.Collections.Generic;

public class NumberASTNode : ASTNode
{
    PrimitiveValue number;
    // 読み出しトークンの型に応じて呼び出しコンストラクタを変えられるようにしておく
    public NumberASTNode(int intValue) { number = PrimitiveValue.makeInt(intValue); }
    public NumberASTNode(float floatValue) { number = PrimitiveValue.makeFloat(floatValue); }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2));
        return instructions;
    }

    public override string Print(int tab)
    {
        try
        {
            return $"{(int)number}";
        }
        catch (Exception e)
        {
            return $"{(float)number}";
        }
    }
}
