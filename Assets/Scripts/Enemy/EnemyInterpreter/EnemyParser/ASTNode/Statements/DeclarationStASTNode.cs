using System;
using System.Collections.Generic;

public class DeclarationStASTNode : StatementASTNode
{
    PrimitiveValue.Type type;
    string type_str;
    string id;
    public DeclarationStASTNode(PrimitiveValue.Type type, string id)
    {
        this.type = type;
        switch (type)
        {
            case PrimitiveValue.Type.INT:
                type_str = "int";
                break;
            case PrimitiveValue.Type.FLOAT:
                type_str = "float";
                break;
            default:
                throw new NotImplementedException($"型 {type} の宣言に対応する動作はASTNodeに実装されていません");
        }
        this.id = id;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        vtable.Add(id, vtable.Count);
        return new List<EnemyVM.Instruction>();
    }

    public override string Print(int tab)
    {
        return GetTabs(tab) + $"{type_str} {id}\n";
    }
}
