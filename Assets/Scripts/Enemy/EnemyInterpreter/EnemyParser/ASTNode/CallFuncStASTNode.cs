using System;
using System.Collections.Generic;
using System.Linq;
public class CallFuncStASTNode : ASTNode
{
    private int id;
    private string functionName;
    private List<ExpASTNode> expSts;

    /** @param exps: リストの昇順と引数を左から順に読んだ結果が一致する想定 */
    public CallFuncStASTNode(int id, string functionName, List<ExpASTNode> exps)
    {
        this.id = id;
        this.functionName = functionName;
        this.expSts = exps;
    }
    private class FunctionIDNotFoundException : Exception
    {
        public FunctionIDNotFoundException() : base() { }
        public FunctionIDNotFoundException(string message) : base(message) { }
        public FunctionIDNotFoundException(string message, Exception innerException) : base(message, innerException) { }
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
        var funcID = EnemyFunctionFactory.GetInstance().Find(functionName);
        if (funcID == -1) { throw new FunctionIDNotFoundException($"function {functionName} not found in function list of EnemyFunctionsFactory"); }
        instructions.Add(
            // CALL functionName
            new EnemyVM.Instruction(
                EnemyVM.Mnemonic.CALL,
                funcID
            )
        );
        return instructions;
    }
    public override string Print(int tab)
    {
    }
}
