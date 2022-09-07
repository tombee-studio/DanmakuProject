using System;
using System.Collections.Generic;

public class BehaviourASTNode: ASTNode
{
    public string id;

    public BehaviourASTNode(string id)
    {
        this.id = id;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        throw new NotImplementedException();
    }

    public override string Print(int tab)
    {
        return $"behaviour {id}";
    }
}
