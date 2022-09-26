using System;
using System.Collections.Generic;
using UnityEngine;
public class UnaryExpASTNode : ExpASTNode
{
    int sign;
    PrimaryExpASTNode primaryExp;
    public UnaryExpASTNode(PrimaryExpASTNode primaryExp)
    {
        this.sign = 0;
        this.primaryExp = primaryExp;
    }
    public UnaryExpASTNode(ScriptToken sign, PrimaryExpASTNode primaryExp)
    {
        this.primaryExp = primaryExp;
        switch (sign.type)
        {
            case ScriptToken.Type.NONE:
                this.sign = 0;
                break;
            case ScriptToken.Type.SUB:
                this.sign = -1;
                break;
            case ScriptToken.Type.PLUS:
                this.sign = 1;
                break;
            default:
                throw new Exception("Unexpected Token received.");
        }
    }
    public static implicit operator UnaryExpASTNode(PrimaryExpASTNode node)
    {
        return new UnaryExpASTNode(node);
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = primaryExp.Compile(vtable);
        if (sign == -1)
        {
            instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, -1));
            instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0));
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        string[] signs = { "-", "", "+" };
        string sign_str = signs[sign + 1];
        return sign_str + primaryExp.Print(tab);
    }
}
