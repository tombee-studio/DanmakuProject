using System;
using System.Collections.Generic;

public class FactorASTNode : ASTNode
{
    private FactorASTNode left;
    private ScriptToken arithmeticOperator;
    private NumberASTNode right;

    public FactorASTNode(FactorASTNode left, ScriptToken arithmeticOperator, NumberASTNode right)
    {
        this.left = left;
        this.arithmeticOperator = arithmeticOperator;
        this.right = right;
    }
    public static implicit operator FactorASTNode(NumberASTNode number){
        var arithmeticOperator = new ScriptToken();
        arithmeticOperator.type = ScriptToken.Type.NONE;
        FactorASTNode factor = new FactorASTNode(null, arithmeticOperator, number);
        return factor;
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
                    instructions.Add(
                        new EnemyVM.Instruction(EnemyVM.Mnemonic.MOD, 2));
                    break;
                default:
                    throw new Exception($"Unexpected Operator {arithmeticOperator} reserved");
            }
        }
        else if (arithmeticOperator.type == ScriptToken.Type.SUB)
        {
            instructions.Add(
                new EnemyVM.Instruction(
                    EnemyVM.Mnemonic.PUSH,
                    PrimitiveValue.makeInt(-1)));
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
        else
        {
            if (arithmeticOperator.type == ScriptToken.Type.SUB)
            {
                str += "-";
            }
        }
        str += right.Print(tab);
        return str;
    }
}
