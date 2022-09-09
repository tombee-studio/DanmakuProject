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
        int address = vtable.Count;
        vtable.Add(id, address);
        List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, address));
        instructions.AddRange(exp.Compile(vtable));
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.STORE, 0));
        return instructions;
    }

    public override string Print(int tab)
    {
        return $"{id} = " + exp.Print(tab);
    }
}
