using System;
using System.Collections.Generic;

public class StatementASTNode : ASTNode
{
    ASTNode child;

    public StatementASTNode() {
    }
    public StatementASTNode(DeclarationStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(AssignStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(ExpStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(IfStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(RepeatStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(BreakStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(CallFuncStASTNode statement)
    {
        this.child = statement;
    }
    public StatementASTNode(BlockStASTNode statement)
    {
        this.child = statement;
    }
    public static implicit operator StatementASTNode(ExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(EqualityExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(RelationalExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(TermExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(FactorExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(UnaryExpASTNode node)
    {
        return new StatementASTNode(node);
    }
    public static implicit operator StatementASTNode(PrimaryExpASTNode node)
    {
        return new StatementASTNode(node);
    }


    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return child.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return child.Print(tab);
    }
}
