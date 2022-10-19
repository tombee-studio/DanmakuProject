using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionASTNode : ActionASTNodeBase
{
    List<StatementASTNodeBase> statements;
    public ActionASTNode(List<StatementASTNodeBase> statements)
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
