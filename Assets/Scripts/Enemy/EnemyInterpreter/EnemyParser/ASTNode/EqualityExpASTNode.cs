using System;
using System.Collections.Generic;

public class EqualityExpASTNode : ASTNode
{
    private EqualityExpASTNode left;
    private ScriptToken relationOperator;
    private RelationalExpASTNode right;

    public EqualityExpASTNode(RelationalExpASTNode relationalExp)
    {
        this.left = null;
        this.relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = relationalExp;
    }
    public EqualityExpASTNode(EqualityExpASTNode left, ScriptToken arithmeticOperator, RelationalExpASTNode right)
    {
        this.left = left;
        this.relationOperator = arithmeticOperator;
        this.right = right;
    }
    public static implicit operator EqualityExpASTNode(RelationalExpASTNode node)
    {
        return new EqualityExpASTNode(node);
    }
    public static implicit operator EqualityExpASTNode(TermExpASTNode node)
    {
        return new EqualityExpASTNode(node);
    }
    public static implicit operator EqualityExpASTNode(FactorExpASTNode node)
    {
        return new EqualityExpASTNode(node);
    }
    public static implicit operator EqualityExpASTNode(UnaryExpASTNode node)
    {
        return new EqualityExpASTNode(node);
    }
    public static implicit operator EqualityExpASTNode(PrimaryExpASTNode node)
    {
        return new EqualityExpASTNode(node);
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
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 2));
                    break;
                case ScriptToken.Type.NOT:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.NE, 2));
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
