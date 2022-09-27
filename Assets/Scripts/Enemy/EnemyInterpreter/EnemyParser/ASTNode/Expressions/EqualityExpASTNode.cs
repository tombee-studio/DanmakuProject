using System;
using System.Collections.Generic;

public class EqualityExpASTNode : EqualityExpASTNodeBase
{
    private EqualityExpASTNodeBase left;
    private ScriptToken relationOperator;
    private RelationalExpASTNodeBase right;

    public EqualityExpASTNode(RelationalExpASTNodeBase relationalExp)
    {
        this.left = null;
        this.relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = relationalExp;
    }
    public EqualityExpASTNode(EqualityExpASTNodeBase left, ScriptToken relationalOperator, RelationalExpASTNodeBase right)
    {
        this.left = left;
        this.relationOperator = relationalOperator;
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
                case ScriptToken.Type.EQUAL:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0));
                    break;
                case ScriptToken.Type.NOT:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.NE, 0));
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
                case ScriptToken.Type.EQUAL:
                    str += "==";
                    break;
                case ScriptToken.Type.NOT:
                    str += "!=";
                    break;
            }
        }
        str += right.Print(tab);
        return str;
    }
}
