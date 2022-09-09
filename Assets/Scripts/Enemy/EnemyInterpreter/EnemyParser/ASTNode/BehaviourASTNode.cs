using System;
using System.Collections.Generic;

public class BehaviourASTNode : ASTNode
{
    public string id;
    private List<ASTNode> bulletAndAction;

    public BehaviourASTNode(string id, BulletASTNode bullet, ActionASTNode action)
    {
        this.id = id;
        this.bulletAndAction = new List<ASTNode>();
        this.bulletAndAction.Add(bullet);
        this.bulletAndAction.Add(action);
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        List<EnemyVM.Instruction> instructions = GetInstructionsForAll(bulletAndAction, vtable);
        for (int i = 0; i < instructions.Count; i++)
        {
            var instruction = instructions[i];
            switch (instruction.mnemonic)
            {
                case EnemyVM.Mnemonic.JMP:
                case EnemyVM.Mnemonic.JE:
                case EnemyVM.Mnemonic.JNE:
                    instruction.argument += i;  // instruction の命令位置を足すことで, 即値を絶対的な値にする
                    break;
                default:
                    break;
            }
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + $"behaviour {id}\n"
        + "{\n"
        + GetMergedString(bulletAndAction, tab + 1)
        + "}";
    }
}
