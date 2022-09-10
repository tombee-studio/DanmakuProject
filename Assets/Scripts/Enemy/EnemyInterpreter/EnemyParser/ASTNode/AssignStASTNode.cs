using System;
using System.Collections.Generic;

public class AssignStASTNode : ASTNode
{
    string id;
    ExpASTNode exp;
    public AssignStASTNode(string id, ExpASTNode exp)
    {
        this.id = id;
        this.exp = exp;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        int address = vtable[id];
        vtable.Add(id, address);
        var instructions = exp.Compile(vtable);
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, address));
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.STORE, 0));
        return instructions;
    }

    public override string Print(int tab)
    {
        return $"{id} = " + exp.Print(tab) + "\n";
    }
}
