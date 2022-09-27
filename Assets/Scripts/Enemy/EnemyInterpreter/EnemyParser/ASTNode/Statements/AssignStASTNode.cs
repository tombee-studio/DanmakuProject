using System;
using System.Collections.Generic;

// TODO: 設定は反映しない
public class AssignStASTNode : AssignStASTNodeBase
{
    string id;
    ExpASTNodeBase exp;
    public AssignStASTNode(string id, ExpASTNodeBase exp)
    {
        this.id = id;
        this.exp = exp;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = exp.Compile(vtable);
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, vtable[id]));
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.STORE, 0));
        return instructions;
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + $"{id} = " + exp.Print(tab) + "\n";
    }
}
