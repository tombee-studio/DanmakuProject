using System;
using System.Collections.Generic;

public class FactorExpASTNode : ExpASTNode
{
    private FactorExpASTNode left;
    private ScriptToken arithmeticOperator;
    private UnaryExpASTNode right;

    public FactorExpASTNode(UnaryExpASTNode unaryExp)
    {
        this.left = null;
        this.arithmeticOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = unaryExp;
    }
    public FactorExpASTNode(FactorExpASTNode left, ScriptToken arithmeticOperator, PrimaryExpASTNode right)
    {
        this.left = left;
        this.arithmeticOperator = arithmeticOperator;
        this.right = right;
    }
    public static implicit operator FactorExpASTNode(UnaryExpASTNode unaryExp)
    {
        return new FactorExpASTNode(unaryExp);
    }
    public static implicit operator FactorExpASTNode(PrimaryExpASTNode primaryExp)
    {
        return new FactorExpASTNode(primaryExp);
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
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0));
                    break;
                case ScriptToken.Type.DIVIDE:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 0));
                    break;
                case ScriptToken.Type.MOD:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.MOD, 0));
                    break;
                default:
                    throw new Exception($"Unexpected Operator {arithmeticOperator} reserved");
            }
        }
        else
        {
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
            switch (arithmeticOperator.type)
            {
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
        str += right.Print(tab);
        return str;
    }
}
