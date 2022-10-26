using System;
using System.Collections.Generic;

// TODO: 設定は反映しない
public class ExpStASTNode : ExpStASTNodeBase
{
    ExpASTNodeBase exp;
    public ExpStASTNode(ExpASTNodeBase exp)
    {
        this.exp = exp;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return exp.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + exp.Print(tab) + "\n";
    }
}
