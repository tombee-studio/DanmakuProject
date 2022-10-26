using System;
using System.Collections.Generic;

public class RelationalExpASTNode : RelationalExpASTNodeBase
{
    private TermExpASTNodeBase left;
    private ScriptToken relationOperator;
    private RelationalExpASTNodeBase right;

    public RelationalExpASTNode(TermExpASTNodeBase termExp)
    {
        this.left = null;
        this.relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = termExp;
    }
    public RelationalExpASTNode(TermExpASTNodeBase left, ScriptToken arithmeticOperator, RelationalExpASTNodeBase right)
    {
        this.left = left;
        this.relationOperator = arithmeticOperator;
        this.right = right;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = new List<EnemyVM.Instruction>();
        if (left != null)
        {
            instructions.AddRange(left.Compile(vtable));
            instructions.AddRange(right.Compile(vtable));
            switch (relationOperator.type)
            {
                case ScriptToken.Type.GREATER_THAN:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.GT, 0));
                    break;
                case ScriptToken.Type.GREATER_EQUAL:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0));
                    break;
                case ScriptToken.Type.LESS_THAN:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0));
                    break;
                case ScriptToken.Type.LESS_EQUAL:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0));
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
            switch (relationOperator.type)
            {
                case ScriptToken.Type.GREATER_THAN:
                    str += ">";
                    break;
                case ScriptToken.Type.GREATER_EQUAL:
                    str += ">=";
                    break;
                case ScriptToken.Type.LESS_THAN:
                    str += "<";
                    break;
                case ScriptToken.Type.LESS_EQUAL:
                    str += "<=";
                    break;
            }
        }
        str += right.Print(tab);
        return str;
    }

}
