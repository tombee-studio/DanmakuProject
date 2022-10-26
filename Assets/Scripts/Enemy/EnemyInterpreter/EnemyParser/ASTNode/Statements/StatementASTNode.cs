using System;
using System.Collections.Generic;

// TODO: 設定は反映しない
public class StatementASTNode : StatementASTNodeBase
{
    ASTNode child;

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

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return child.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return child.Print(tab);
    }
}
