using System;
using System.Collections.Generic;
using System.Linq;

public class BulletSectionASTNode : BulletSectionASTNodeBase
{
    private int id;
    private List<CallFuncStASTNodeBase> callFuncs;
    public BulletSectionASTNode(int id, List<CallFuncStASTNodeBase> callFuncs)
    {
        this.id = id;
        this.callFuncs = callFuncs;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        callFuncs.ForEach(e => ((CallFuncStASTNode)e).id = id);  // id を登録
        return GetInstructionsForAll(callFuncs, vtable);
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + $"ID: {id}\n"
            + GetMergedString(callFuncs, tab);
    }
}
