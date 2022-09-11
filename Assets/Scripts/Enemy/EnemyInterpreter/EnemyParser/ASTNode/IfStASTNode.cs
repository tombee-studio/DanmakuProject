using System;
using System.Collections.Generic;
using System.Linq;
public class IfStASTNode : ASTNode
{
    ExpASTNode cond;
    StatementASTNode ifBody;
    StatementASTNode elseBody;
    public IfStASTNode(
        ExpASTNode cond,
        StatementASTNode ifBody,
        StatementASTNode elseBody
    )
    {
        this.cond = cond;
        this.ifBody = ifBody;
        this.elseBody = elseBody;
    }
    public IfStASTNode(
        ExpASTNode cond,
        StatementASTNode ifBody
    )
    {
        this.cond = cond;
        this.ifBody = ifBody;
        this.elseBody = null;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var ifBodyInstructions = ifBody.Compile(vtable);
        int elseAddress = ifBodyInstructions.Count  // ジャンプ先を相対位置で指定
            + ((elseBody != null) ? 1 : 0);  // else の直前の JMP を飛び越える

        var instructions
            = cond.Compile(vtable)
                .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.JNE, elseAddress))
                .Concat(ifBodyInstructions);
        if (elseBody != null)
        {
            var elseBodyInstructions = elseBody.Compile(vtable);
            int nextAddress = elseBodyInstructions.Count;
            return instructions
                .Append(new EnemyVM.Instruction(EnemyVM.Mnemonic.JMP, nextAddress))
                .Concat(elseBody.Compile(vtable))
                .ToList();
        }

        return instructions.ToList();
    }

    public override string Print(int tab)
    {
        var original = GetTabs(tab) + "if(" + cond.Print(tab) + ")" + ifBody.Print(tab);
        if (elseBody != null)
        {
            return original + " else " + elseBody.Print(tab);
        }

        return original;
    }
}
