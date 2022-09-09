using System;
using System.Collections.Generic;

public class StatementASTNode : ASTNode
{
    ASTNode child;
    public StatementASTNode(ASTNode child)
    {
        this.child = child;
    }

    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        /*
            List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
            instructions.Add(EnemyVM.Mnemonic.PUSH, 0x7fffffff);  // 明らかにエラーな値をプッシュしておく
            instructions.AddRange(child.Compile(vtable));
            instructions.Add(EnemyVM.Mnemonic.POP, 0);  // スタックに入れた値は破棄しておく;
            return instructions;
        */
        return child.Compile(vtable);
    }

    public override string Print(int tab)
    {
        return child.Print(tab);
    }
}
