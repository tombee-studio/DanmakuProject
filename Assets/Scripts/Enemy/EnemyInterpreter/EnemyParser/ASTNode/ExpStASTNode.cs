using System;
using System.Collections.Generic;

public class ExpStASTNode: ASTNode
{
    ExpASTNode exp;
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return exp.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return exp.Print(tab) + "\n";
    }
}
