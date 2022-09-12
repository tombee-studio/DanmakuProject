using System;
using System.Collections.Generic;
using System.Linq;
public class ActionASTNode : ASTNode
{
    List<StatementASTNode> statements;
    public ActionASTNode(List<StatementASTNode> statements)
    {
        this.statements = statements;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return GetInstructionsForAll(statements, vtable);
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + "action >>\n"
            + GetMergedString(statements, tab);
    }
}
