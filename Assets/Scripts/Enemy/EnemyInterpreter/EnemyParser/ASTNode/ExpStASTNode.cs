using System;
using System.Collections.Generic;

public class ExpStASTNode: ASTNode
{
    ExpASTNode exp;
    public ExpStASTNode(ExpASTNode exp){
        this.exp = exp;
    }
    public static implicit operator ExpStASTNode(ExpASTNode node){
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(EqualityExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(RelationalExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(TermExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(FactorExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(UnaryExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public static implicit operator ExpStASTNode(PrimaryExpASTNode node)
    {
        return new ExpStASTNode(node);
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return exp.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return exp.Print(tab) + "\n";
    }
}
