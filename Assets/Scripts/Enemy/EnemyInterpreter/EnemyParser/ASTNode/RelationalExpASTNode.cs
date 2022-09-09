using System;
using System.Collections.Generic;

public class RelationalExpASTNode : ASTNode
{
    private RelationalExpASTNode left;
    private ScriptToken relationOperator;
    private TermExpASTNode right;

    public RelationalExpASTNode(TermExpASTNode termExp)
    {
        this.left = null;
        this.relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = termExp;
    }
    public RelationalExpASTNode(RelationalExpASTNode left, ScriptToken arithmeticOperator, TermExpASTNode right)
    {
        this.left = left;
        this.relationOperator = arithmeticOperator;
        this.right = right;
    }
    public static implicit operator RelationalExpASTNode(TermExpASTNode node)
    {
        return new RelationalExpASTNode(node);
    }
    public static implicit operator RelationalExpASTNode(FactorExpASTNode node)
    {
        return new RelationalExpASTNode(node);
    }
    public static implicit operator RelationalExpASTNode(UnaryExpASTNode node)
    {
        return new RelationalExpASTNode(node);
    }
    public static implicit operator RelationalExpASTNode(PrimaryExpASTNode node)
    {
        return new RelationalExpASTNode(node);
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
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.GT, 2));
                    break;
                case ScriptToken.Type.GREATER_EQUAL:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 2));
                    break;
                case ScriptToken.Type.LESS_THAN:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 2));
                    break;
                case ScriptToken.Type.LESS_EQUAL:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 2));
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
