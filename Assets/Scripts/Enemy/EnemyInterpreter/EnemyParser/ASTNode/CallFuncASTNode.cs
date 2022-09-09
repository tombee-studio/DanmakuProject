using System;
using System.Collections.Generic;
using System.Linq;
public class CallFuncASTNode : ASTNode
{
    private string id;
    private List<ExpStASTNode> expSts;

    public CallFuncASTNode(string id, List<ExpStASTNode> expSts)
    {
        this.id = id;
        this.expSts = expSts;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        throw new NotImplementedException("関数呼び出しはまだ実装されていません");
        return GetInstructionsForAll(expSts, vtable);
    }

    public override string Print(int tab)
    {
        return $"{id}(" + GetMergedString(expSts, tab) + ")\n";
    }
}
