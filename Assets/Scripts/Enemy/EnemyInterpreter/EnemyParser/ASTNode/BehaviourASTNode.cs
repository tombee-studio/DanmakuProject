using System;
using System.Collections.Generic;
using System.Linq;
public class BehaviourASTNode : BehaviourASTNodeBase
{
    public string id;
    private List<ASTNode> bulletAndAction;

    public BehaviourASTNode(string id, BulletASTNodeBase bullet, ActionASTNodeBase action)
    {
        this.id = id;
        this.bulletAndAction = new List<ASTNode>();
        if (bullet != null) {
            this.bulletAndAction.Add(bullet);
        }
        this.bulletAndAction.Add(action);
    }
    public BehaviourASTNode(string id, ActionASTNodeBase action)
    {
        this.id = id;
        this.bulletAndAction = new List<ASTNode>();
        this.bulletAndAction.Add(action);
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var subProgram = GetInstructionsForAll(bulletAndAction, vtable);

        var stackAllocation = vtable.Select(e => new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 0));

        var program = stackAllocation
            .Concat(subProgram)
            .Select((instruction, line) =>
            {
                switch (instruction.mnemonic)
                {
                    case EnemyVM.Mnemonic.JMP:
                    case EnemyVM.Mnemonic.JE:
                    case EnemyVM.Mnemonic.JNE:
                        instruction.argument += line;  // instruction の命令位置を足すことで, 即値を絶対的な値にする
                        return instruction;
                    default:
                        return instruction;
                }
            }).ToList();
        return program;
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + $"behaviour {id}\n"
        + GetTabs(tab) + "{\n"
        + GetMergedString(bulletAndAction, tab + 1)
        + GetTabs(tab) + "}";
    }
}
