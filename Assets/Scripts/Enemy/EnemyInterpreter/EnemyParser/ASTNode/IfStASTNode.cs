using System;
using System.Collections.Generic;

public class IfStASTNode : ASTNode
{
    ExpStASTNode cond;
    StatementASTNode ifBody;
    StatementASTNode elseBody;
    public IfStASTNode(
        ExpASTNode cond,
        StatementASTNode ifBody,
        StatementASTNode elseBody
    )
    {

    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        List<EnemyVM.Instruction> ifBodyInstructions = ifBody.Compile(vtable);
        int elseAddress = ifBodyInstructions.Count - 1;  // ジャンプ先を相対位置で指定

        List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
        instructions.AddRange(cond.Compile(vtable));
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 0));
        instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.JE, elseAddress));
        instructions.AddRange(ifBody.Compile(vtable));
        instructions.AddRange(elseBody.Compile(vtable));
        //TODO:
        /*
        stack -> cond
        PUSH 0
        JE :else
        {}
        :else
        */
        throw new NotImplementedException();
    }

    public override string Print(int tab)
    {
        throw new NotImplementedException();
    }
}
