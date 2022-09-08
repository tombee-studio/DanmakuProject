using System;
using System.Collections.Generic;

public class FactorASTNode : ASTNode
{
    private FactorASTNode left;
    private NumberASTNode right;
    private ScriptToken arithmeticOperator;

    public FactorASTNode(ScriptToken arithmeticOperator, FactorASTNode left, NumberASTNode right)
    {
        this.arithmeticOperator = arithmeticOperator;
        this.left = left;
        this.right = right;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = new List<EnemyVM.Instruction>();
        if (left != null)
        {
            instructions.AddRange(left.Compile(vtable));
            instructions.AddRange(right.Compile(vtable));
            switch (arithmeticOperator.type)
            {
                case ScriptToken.Type.MULTIPLY:
                    instructions.Add(
                        new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 2));
                    break;
                case ScriptToken.Type.DIVIDE:
                    instructions.Add(
                        new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 2));
                    break;
                case ScriptToken.Type.MOD:
                    throw new NotImplementedException("MOD演算子を実装しよう");
            }
        }
        else if (arithmeticOperator.type == ScriptToken.Type.SUB)
        {
            instructions.Add(
                new EnemyVM.Instruction(
                    EnemyVM.Mnemonic.PUSH,
                    PrimitiveValue.makeInt(-1)));
        }
        else {
            instructions.AddRange(right.Compile(vtable));
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        string str = "";
        if (left != null)
        {
            str = left.Print(tab);
            switch (arithmeticOperator.type) {
                case ScriptToken.Type.MULTIPLY:
                    str += "*";
                    break;
                case ScriptToken.Type.DIVIDE:
                    str += "/";
                    break;
                case ScriptToken.Type.MOD:
                    str += "%";
                    break;
            }
        }
        else {
            if (arithmeticOperator.type == ScriptToken.Type.SUB) {
                str += "-";
            }
        }
        str += right.Print(tab);
        return str;
    }
}
