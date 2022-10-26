using System;
using System.Collections.Generic;
using System.Linq;

// TODO: 設定は反映しない
public class IfStASTNode : IfStASTNodeBase
{
    ExpASTNodeBase cond;
    StatementASTNodeBase ifBody;
    StatementASTNodeBase elseBody;
    public IfStASTNode(
        ExpASTNodeBase cond,
        StatementASTNodeBase ifBody,
        StatementASTNodeBase elseBody
    )
    {
        this.cond = cond;
        this.ifBody = ifBody;
        this.elseBody = elseBody;
    }
    public IfStASTNode(
        ExpASTNodeBase cond,
        StatementASTNodeBase ifBody
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
        var original = GetTabs(tab) + "if(" + cond.Print(tab) + ")\n" 
            + ifBody.Print(tab+1);
        if (elseBody != null)
        {
            return original + "else\n" 
                + elseBody.Print(tab+1);
        }

        return original;
    }
}
