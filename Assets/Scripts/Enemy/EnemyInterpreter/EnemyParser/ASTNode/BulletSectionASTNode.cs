using System;
using System.Collections.Generic;
using System.Linq;

public class BulletSectionASTNode : ASTNode
{
    private int id;
    private List<CallFuncASTNode> callFuncs;
    public BulletSectionASTNode(int id, List<CallFuncASTNode> callFuncs)
    {
        this.id = id;
        this.callFuncs = callFuncs;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return GetInstructionsForAll(callFuncs, vtable);
    }

    public override string Print(int tab)
    {
        return $"ID: {id}\n"
            + GetMergedString(callFuncs, tab);
    }
}
