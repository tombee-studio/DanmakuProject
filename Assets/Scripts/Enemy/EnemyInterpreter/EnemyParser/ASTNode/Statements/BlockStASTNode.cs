using System;
using System.Collections.Generic;
using System.Linq;

public class BlockStASTNode : StatementASTNode
{
    List<StatementASTNode> statements;
    public BlockStASTNode(List<StatementASTNode> statements)
    {
        this.statements = statements;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return statements.SelectMany(e => e.Compile(vtable)).ToList();
    }

    public override string Print(int tab)
    {
        return "\n"
            + GetTabs(tab) + "{\n"
            + String.Join("", statements.SelectMany(e => e.Print(tab + 1)))
            + GetTabs(tab) + "}\n";
    }
}
