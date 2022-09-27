using System;
using System.Collections.Generic;
using System.Linq;
public partial class EnemyASTNodeTester
{
    void test_BulletActionJointInBehaviorASTNodeTester(){
        string[] scriptCodes = {
            "behaviour BulletAndActionJointTest",
            "{",
            "\t" + "bullet >>",
            "\t" + "ID: 0",
            "\t" + "generate_bullets(24)",
            "\t" + "set_bullets_position_at_enemy()",

            "\t" + "ID: 1",
            "\t" + "generate_bullets(48)",
            "\t" + "scatter_bullets_in_circular_pattern(0.1, 0)",

            "\t" + "action >>",
            "\t" + "activate_bullets(0)",
            "\t" + "activate_bullets(1)",
            "}"
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

            "PUSH 0",
            $"CALL {fFactory.Find("activate_bullets")}",
            "PUSH 1",
            $"CALL {fFactory.Find("activate_bullets")}",
        };
        var node = new BehaviourASTNode(
            "BulletAndActionJointTest",
            new BulletASTNode(
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
            ),
            new ActionASTNode(
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
            )
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        checkPrintScript(scriptCodes, node);
    }
    void test_VMRunInBehaviorASTNodeTester()
    {
        // 1 から 10 までの総和を求める
        string[] scriptCodes = {
            "behaviour VMRunTest",
            "{",
            "\t" + "action >>",
            "\t" + "int i",
            "\t" + "int sum",
            "\t" + "i = 1",
            "\t" + "sum = 0",
            "\t" + "repeat(10)",
            "\t" + "{",
            "\t" + "\t" + "if(i>6)",
            "\t" + "\t" + "\t" + "break",
            "\t" + "\t" + "sum = sum+i",
            "\t" + "\t" + "i = i+1",
            "\t" + "}",
            "\t" + "sum",
            "}",
            ""
        };
        string[] testCodes = {
            // int i
            "PUSH 0",   // 0
            // int sum
            "PUSH 0",   // 1
            // int counter
            "PUSH 0",   // 2
            // i = 1
            "PUSH 1",   // 3
            "PUSH 0",   // 4
            "STORE 0",  // 5
            // sum = 0
            "PUSH 0",   // 6
            "PUSH 1",   // 7
            "STORE 0",  // 8
            // counter = 0
            "PUSH 0",   // 9
            "PUSH 2",   // 10
            "STORE 0",  // 11
            // :loop
            // if (not(counter < 10)) goto :end_loop
            "PUSH 2",   // 12
            "LOAD 0",   // 13
            "PUSH 10",  // 14
            "LT 0",     // 15
            "JNE 42",   // 16
            // if(i<6) {
            "PUSH 0",   // 17
            "LOAD 0",   // 18
            "PUSH 6",   // 19
            "GT 0",     // 20
            "JNE 22",    // 21
            // break; 
            "JMP 42",   // 22
            // }
            // sum = sum + i
            "PUSH 1",   // 23
            "LOAD 0",   // 24
            "PUSH 0",   // 25
            "LOAD 0",   // 26
            "ADD 0",    // 27
            "PUSH 1",   // 28
            "STORE 0",  // 29
            // i = i + 1
            "PUSH 0",   // 30
            "LOAD 0",   // 31
            "PUSH 1",   // 32
            "ADD 0",    // 33
            "PUSH 0",   // 34
            "STORE 0",  // 35
            // counter++
            "PUSH 2",   // 36
            "LOAD 0",   // 37
            "PUSH 1",   // 38
            "ADD 0",    // 39
            "PUSH 2",   // 40
            "STORE 0",  // 41
            // goto :loop
            "JMP 11",  // 42
            // :end_loop
            "PUSH 1",   // 43
            "LOAD 0",   // 44
        };
        var node = new BehaviourASTNode("VMRunTest",
            new ActionASTNode(
                new List<StatementASTNodeBase>()
                .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "i"))
                .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "sum"))
                .Append(new AssignStASTNode("i", new PrimaryExpASTNode(1)))
                .Append(new AssignStASTNode("sum", new PrimaryExpASTNode(0)))
                .Append(new RepeatStASTNode(10, new BlockStASTNode(
                    new List<StatementASTNodeBase>()
                    .Append(new IfStASTNode(
                        new RelationalExpASTNode(
                            new PrimaryExpASTNode("i"),
                            ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN),
                            new PrimaryExpASTNode(6)
                        ),
                        new BreakStASTNode()
                    ))
                    .Append(new AssignStASTNode("sum",
                        new TermExpASTNode(
                            new PrimaryExpASTNode("sum"),
                            ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                            new PrimaryExpASTNode("i")
                        )
                    ))
                    .Append(new AssignStASTNode("i",
                        new TermExpASTNode(
                            new PrimaryExpASTNode("i"),
                            ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                            new PrimaryExpASTNode(1)
                        )
                    ))
                    .ToList()
                )))
                .Append(
                    new ExpStASTNode(
                        new PrimaryExpASTNode("sum")
                    )
                )
                .ToList()
            )
        );
        checkPrintScript(scriptCodes, node);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValue(node, 21);
    }
}

