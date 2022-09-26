using System;
using System.Collections.Generic;

public class ExpASTNode : ASTNode
{
    EqualityExpASTNode equalityExp;
    public ExpASTNode(){
        this.equalityExp = null;
    }
    public ExpASTNode(EqualityExpASTNode equalityExp){
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
