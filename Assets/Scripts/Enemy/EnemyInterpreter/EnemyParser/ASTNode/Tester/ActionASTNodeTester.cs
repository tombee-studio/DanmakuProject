using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public partial class EnemyASTNodeTester : Tester
{
    void test_ActionASTNodeTester()
    {
        string[] scriptCodes = {
            "action >>",
            "activate_bullets(0)",
            "activate_bullets(1)",
        };
        var fFactory = EnemyFunctionFactory.GetInstance();
        string[] testCodes = {
            "PUSH 0",
            $"CALL {fFactory.Find("activate_bullets")}",
            "PUSH 1",
            $"CALL {fFactory.Find("activate_bullets")}",
        };
        var node = new ActionASTNode(
            new List<StatementASTNodeBase>()
            .Append(new CallFuncStASTNode(
                "activate_bullets",
                new List<ExpASTNodeBase>()
                .Append(new PrimaryExpASTNode(0))
                .ToList()
                ))
            .Append(new CallFuncStASTNode(
                "activate_bullets",
                new List<ExpASTNodeBase>()
                .Append(new PrimaryExpASTNode(1))
                .ToList()
                ))
            .ToList()
        );
        int i = 0;
        node.Compile(new Dictionary<string, int>())
            .ForEach(e=>Debug.Log($"{e}, {i++}"));
        checkGeneratedInstructionIsSame(testCodes, node);
        checkPrintScript(scriptCodes, node);
    }
}