using System;
using System.Collections.Generic;
using System.Linq;
public partial class EnemyASTNodeTester
{
    void test_BulletASTNodeTester(){
        string[] scriptCodes = {
            "bullet >>",
            "ID: 0",
            "generate_bullets(24)",
            "set_bullets_position_at_enemy()",

            "ID: 1",
            "generate_bullets(48)",
            "scatter_bullets_in_circular_pattern(0.1, 0)",
            ""
        };
        var fFactory = EnemyFunctionFactory.GetInstance();
        string[] testCodes = {
            "PUSH 24",
            "PUSH 0",
            $"CALL {fFactory.Find("generate_bullets")}",
            "PUSH 0",
            $"CALL {fFactory.Find("set_bullets_position_at_enemy")}",

            "PUSH 48",
            "PUSH 1",
            $"CALL {fFactory.Find("generate_bullets")}",
            "PUSH 0.1",
            "PUSH 0",
            "PUSH 1",
            $"CALL {fFactory.Find("scatter_bullets_in_circular_pattern")}",
        };
        var node = new BulletASTNode(
            new List<BulletSectionASTNodeBase>()
            .Append(new BulletSectionASTNode(0,
                new List<CallFuncStASTNodeBase>()
                    .Append(new CallFuncStASTNode(
                        "generate_bullets", 
                        new List<ExpASTNodeBase>()
                            .Append(new PrimaryExpASTNode(24))
                            .ToList()
                    ))
                    .Append(new CallFuncStASTNode(
                        "set_bullets_position_at_enemy",
                        new List<ExpASTNodeBase>()
                    ))
                    .ToList()
            ))
            .Append(new BulletSectionASTNode(1,
                new List<CallFuncStASTNodeBase>()
                    .Append(new CallFuncStASTNode(
                        "generate_bullets", 
                        new List<ExpASTNodeBase>()
                            .Append(new PrimaryExpASTNode(48))
                            .ToList()
                    ))
                    .Append(new CallFuncStASTNode(
                        "scatter_bullets_in_circular_pattern",
                        new List<ExpASTNodeBase>()
                            .Append(new PrimaryExpASTNode(0.1f))
                            .Append(new PrimaryExpASTNode(0f))
                            .ToList()
                    ))
                    .ToList()
            ))
            .ToList()
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        checkPrintScript(scriptCodes, node);
    }
}

