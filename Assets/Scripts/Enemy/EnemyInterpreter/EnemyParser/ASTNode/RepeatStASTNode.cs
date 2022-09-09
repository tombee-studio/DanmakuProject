using System;
using System.Collections.Generic;

public class RepeatStASTNode : ASTNode
{
    ExpASTNode exp;
    List<StatementASTNode> statements;
    public RepeatStASTNode(ExpASTNode exp, List<StatementASTNode> statements){
        this.exp = exp;
        this.statements = statements;
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
