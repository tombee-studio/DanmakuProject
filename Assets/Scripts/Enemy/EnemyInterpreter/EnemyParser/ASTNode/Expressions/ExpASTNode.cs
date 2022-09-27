using System;
using System.Collections.Generic;

public class ExpASTNode : ExpASTNodeBase
{
    EqualityExpASTNodeBase equalityExp;
    public ExpASTNode(EqualityExpASTNodeBase equalityExp){
        this.equalityExp = equalityExp;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        return equalityExp.Compile(vtable);
    }
    public override string Print(int tab)
    {
        return equalityExp.Print(tab);
    }
}
