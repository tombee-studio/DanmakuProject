using System;
using System.Collections.Generic;

public class DeclarationStASTNode : ASTNode
{
    string id;
    ExpASTNode exp;
    public DeclarationStASTNode(string id, ExpASTNode exp)
    {
        this.id = id;
        this.exp = exp;
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
