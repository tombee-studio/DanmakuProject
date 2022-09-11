using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_ifOnlyIfStASTNodeTester()
    {
        PrimitiveValue cond = 1;
        PrimitiveValue ifBody1 = 2;
        PrimitiveValue ifBody2 = 3;
        string[] testCodes = {
            $"PUSH {cond}",
            "JNE 3",
            $"PUSH {ifBody1}",
            $"PUSH {ifBody2}",
            "MUL 0",
        };
        var node = new IfStASTNode(
            new PrimaryExpASTNode(cond),
            new FactorExpASTNode(
                new PrimaryExpASTNode(ifBody1),
                ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY),
                new PrimaryExpASTNode(ifBody2)
            )
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(node.Print(0), 
            $"if({cond})\n"
            +"\t" + $"{ifBody1}*{ifBody2}\n");
        checkVMReturnValueFromSubProgram(node, ifBody1 * ifBody2);
    }
    void test_ifAndElseIfStASTNodeTester()
    {
        PrimitiveValue cond = 0;
        PrimitiveValue ifBody1 = 5;
        PrimitiveValue ifBody2 = 6;
        PrimitiveValue elseBody = 7;
        string[] testCodes = {
            $"PUSH {cond}",
            "JNE 4",
            $"PUSH {ifBody1}",
            $"PUSH {ifBody2}",
            "MUL 0",
            "JMP 1",
            $"PUSH {elseBody}",
        };
        var node = new IfStASTNode(
            new PrimaryExpASTNode(cond),
            new FactorExpASTNode(
                new PrimaryExpASTNode(ifBody1),
                ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY),
                new PrimaryExpASTNode(ifBody2)
            ),
            new PrimaryExpASTNode(elseBody)
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(node.Print(0), 
        $"if({cond})\n"
        + "\t" + $"{ifBody1}*{ifBody2}\n"
        + "else\n"
        + "\t" + $"{elseBody}\n");
        checkVMReturnValueFromSubProgram(node, cond != 0 ? ifBody1 * ifBody2 : elseBody);
    }
}

