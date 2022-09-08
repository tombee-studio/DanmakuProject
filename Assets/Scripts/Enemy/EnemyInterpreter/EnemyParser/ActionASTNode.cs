using System;
using System.Collections.Generic;

public class ActionASTNode : ASTNode
{
    public ActionASTNode() { }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        throw new NotImplementedException();
    }

    public override string Print(int tab) {
        throw new NotImplementedException();
    }
}
