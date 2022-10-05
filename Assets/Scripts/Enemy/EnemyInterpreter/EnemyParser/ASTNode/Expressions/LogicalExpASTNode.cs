using System;
using System.Collections.Generic;
using UnityEngine;

public class LogicalExpASTNode : LogicalExpASTNodeBase
{
    private EqualityExpASTNodeBase left;
    private ScriptToken relationOperator;
    private LogicalExpASTNodeBase right;

    public LogicalExpASTNode(EqualityExpASTNodeBase equalityExp)
    {
        this.left = null;
        this.relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        this.right = equalityExp;
    }
    public LogicalExpASTNode(EqualityExpASTNodeBase left, ScriptToken equalityOperator, LogicalExpASTNodeBase right)
    {
        this.left = left;
        this.relationOperator = equalityOperator;
        this.right = right;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = new List<EnemyVM.Instruction>();
        if (left != null)
        {
            instructions.AddRange(left.Compile(vtable));
            instructions.AddRange(right.Compile(vtable));
            switch (relationOperator.type)
            {
                case ScriptToken.Type.AND:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.AND, 0));
                    break;
                case ScriptToken.Type.OR:
                    instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.OR, 0));
                    break;
            }
        }
        else
        {
            instructions.AddRange(right.Compile(vtable));
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        string str = "";
        if (left != null)
        {
            str = left.Print(tab);
            switch (relationOperator.type)
            {
                case ScriptToken.Type.AND:
                    str += "and";
                    break;
                case ScriptToken.Type.OR:
                    str += "or";
                    break;
            }
        }
        str += right.Print(tab);
        return str;
    }
}
