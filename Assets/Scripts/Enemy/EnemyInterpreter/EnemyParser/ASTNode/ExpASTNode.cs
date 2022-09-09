using System;
using System.Collections.Generic;

public class ExpASTNode : ASTNode
{
    ASTNode child;
    public ExpASTNode (ASTNode child){
        this.child = child;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        throw new NotImplementedException();
    }

    public override string Print(int tab)
    {
        throw new NotImplementedException();
    }
}
