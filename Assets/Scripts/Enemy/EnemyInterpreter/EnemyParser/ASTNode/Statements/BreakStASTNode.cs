using System;
using System.Collections.Generic;
using System.Linq;

public class BreakStASTNode : ASTNode
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
