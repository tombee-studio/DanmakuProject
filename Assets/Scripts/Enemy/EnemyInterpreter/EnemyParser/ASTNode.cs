using System;
using System.Collections.Generic;

public abstract class ASTNode
{
    public abstract string Print(int tab);
    public abstract List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable);
}
