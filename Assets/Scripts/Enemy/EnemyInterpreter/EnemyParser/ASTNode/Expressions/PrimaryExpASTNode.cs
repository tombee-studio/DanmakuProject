using System;
using System.Collections.Generic;

public class PrimaryExpASTNode : ASTNode
{
    class PrimaryValueException : Exception
    {
        public PrimaryValueException(string message) : base(message) { }
    }
    bool isId = false;
    bool hasParen = false;
    string id;
    PrimitiveValue number;
    ExpASTNode exp = null;
    // 読み出しトークンの型に応じて呼び出しコンストラクタを変えられるようにしておく
    public PrimaryExpASTNode(PrimitiveValue value) { number = value; }
    public PrimaryExpASTNode(string id)
    {
        this.id = id;
        isId = true;
    }
    public PrimaryExpASTNode(ExpASTNode exp)
    {
        this.exp = exp;
        this.hasParen = true;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        List<EnemyVM.Instruction> instructions = new List<EnemyVM.Instruction>();
        if (isId)
        {
            instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, vtable[id]));
            instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.LOAD, 0));
        }
        else if (hasParen)
        {
            instructions.AddRange(exp.Compile(vtable));
        }
        else
        {
            instructions.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, number));
        }
        return instructions;
    }

    public override string Print(int tab)
    {
        if (isId)
        {
            return $"{id}";
        }
        else if (hasParen)
        {
            return "(" + exp.Print(tab) + ")";
        }
        switch (number.type)
        {
            case PrimitiveValue.Type.INT:
                return $"{(int)number}";
            case PrimitiveValue.Type.FLOAT:
                return $"{(float)number}";
            default:
                throw new PrimaryValueException($"Illegal type {number.type} received.");
        }
    }
}
