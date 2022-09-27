using System;
using System.Collections.Generic;

public class TermExpASTNode : TermExpASTNodeBase
{
    private TermExpASTNodeBase left;
    private ScriptToken arithmeticOperator;
    private FactorExpASTNodeBase right;

    public TermExpASTNode(FactorExpASTNodeBase factorExp)
    {
        this.left = null;
        this.arithmeticOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = factorExp;
    }
    public TermExpASTNode(TermExpASTNodeBase left, ScriptToken arithmeticOperator, FactorExpASTNodeBase right)
    {
        this.left = left;
        this.arithmeticOperator = arithmeticOperator;
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
                case ScriptToken.Type.PLUS:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0));
                    break;
                case ScriptToken.Type.SUB:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.SUB, 0));
                    break;
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
                case ScriptToken.Type.PLUS:
                    str += "+";
                    break;
                case ScriptToken.Type.SUB:
                    str += "-";
                    break;
            }
        }
        str += right.Print(tab);
        return str;
    }
}
