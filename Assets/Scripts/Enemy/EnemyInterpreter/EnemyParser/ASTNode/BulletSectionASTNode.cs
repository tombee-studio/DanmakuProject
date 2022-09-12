using System;
using System.Collections.Generic;
using System.Linq;

public class BulletSectionASTNode : ASTNode
{
    private int id;
    private List<CallFuncStASTNode> callFuncs;
    public BulletSectionASTNode(int id, List<CallFuncStASTNode> callFuncs)
    {
        this.id = id;
        this.callFuncs = callFuncs;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        callFuncs.ForEach(e => e.id = id);  // id を登録
        return GetInstructionsForAll(callFuncs, vtable);
    }

    public override string Print(int tab)
    {
        return $"ID: {id}\n"
            + GetMergedString(callFuncs, tab);
    }
}
