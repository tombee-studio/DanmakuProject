﻿using System;
using System.Collections.Generic;
using UnityEngine;
public class UnaryExpASTNode : UnaryExpASTNodeBase
{
    int sign;
    PrimaryExpASTNodeBase primaryExp;
    public UnaryExpASTNode(PrimaryExpASTNodeBase primaryExp)
    {
        this.sign = 0;
        this.primaryExp = primaryExp;
    }
    public UnaryExpASTNode(ScriptToken sign, PrimaryExpASTNodeBase primaryExp)
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
            case ScriptToken.Type.NOT:
                this.sign = 2;
                break;
            default:
                throw new Exception("Unexpected Token received.");
        }
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = primaryExp.Compile(vtable);
        switch(sign){
            case -1:
                instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, -1));
                instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0));
                break;
            // case 0: DoNothing();
            // case 1: DONothing();
            case 2:
                instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.NOT, 0));
                break;
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        string[] signs = { "-", "", "+", "not" };
        string sign_str = signs[sign + 1];
        return sign_str + primaryExp.Print(tab);
    }
}
