using System;
using System.Collections.Generic;

public class ExpASTNode : ASTNode
{
    EqualityExpASTNode equalityExp;
    public ExpASTNode(EqualityExpASTNode equalityExp){
        this.equalityExp = equalityExp;
    }
    public static implicit operator ExpASTNode(EqualityExpASTNode node)
    {
        return new ExpASTNode(node);
    }
    public static implicit operator ExpASTNode(RelationalExpASTNode node)
    {
        return new ExpASTNode(node);
    }
    public static implicit operator ExpASTNode(TermExpASTNode node)
    {
        return new ExpASTNode(node);
    }
    public static implicit operator ExpASTNode(FactorExpASTNode node)
    {
        return new ExpASTNode(node);
    }
    public static implicit operator ExpASTNode(UnaryExpASTNode node)
    {
        return new ExpASTNode(node);
    }
    public static implicit operator ExpASTNode(PrimaryExpASTNode node)
    {
        return new ExpASTNode(node);
    }    
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return equalityExp.Compile(vtable);
    }
    public override string Print(int tab)
    {
        return equalityExp.Print(tab);
    }
}
