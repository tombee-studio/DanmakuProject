using System;
using System.Collections.Generic;

public class BulletASTNode : ASTNode
{
    public BulletASTNode() { }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        throw new NotImplementedException();
    }

    public override string Print(int tab) {
        throw new NotImplementedException();
    }
}
