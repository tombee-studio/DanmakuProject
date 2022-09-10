using System;
using System.Collections.Generic;
using System.Linq;
public class CallFuncASTNode : ASTNode
{
    private int id;
    private string functionName;
    private List<ExpASTNode> expSts;

    /** @param exps: リストの昇順と引数を左から順に読んだ結果が一致する想定 */
    public CallFuncASTNode(int id, string functionName, List<ExpASTNode> exps)
    {
        this.id = id;
        this.functionName = functionName;
        this.expSts = exps;
    }
    public override List<EnemyVM.Instruction> Compile(Dictionary<string, int> vtable)
    {
        var instructions = GetInstructionsForAll(expSts, vtable);  // リストの昇順と引数を左から順に読んだ結果が一致する想定
        instructions.Add(
            new EnemyVM.Instruction(
                EnemyVM.Mnemonic.PUSH,
                id
            )
        );
        instructions.Add(
            // CALL functionName
            new EnemyVM.Instruction(
                EnemyVM.Mnemonic.CALL,
                EnemyFunctionFactory.GetInstance().Find(functionName)
            )
        );
        return instructions;
    }
    public override string Print(int tab)
    {
        return $"{functionName}(" + GetMergedString(expSts, tab, ", ") + ")\n";
    }
}
