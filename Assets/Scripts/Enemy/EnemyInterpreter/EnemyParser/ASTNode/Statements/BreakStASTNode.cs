using System;
using System.Collections.Generic;
using System.Linq;

// TODO: 設定は反映しない
public class BreakStASTNode : BreakStASTNodeBase
{
    public BreakStASTNode(){}
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = new List<EnemyVM.Instruction>();
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.BREAK, 0));
        return instructions;
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + "break\n";
    }
}
