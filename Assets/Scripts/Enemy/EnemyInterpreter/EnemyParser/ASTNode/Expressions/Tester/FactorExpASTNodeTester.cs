using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public partial class EnemyASTNodeTester
{
    public void test_MulFactorExpASTNode()
    {
        string[] testCodes = {
            "PUSH 7",
            "PUSH 13",
            "MUL 2"
        };
        var arithmeticOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY);
        var left = new PrimaryExpASTNode(7);
        var right = new PrimaryExpASTNode(13);
        var node = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(node.Print(0), "7*13");
        checkVMReturnValueFromSubProgram(node, 7 * 13);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_DivFactorExpASTNode()
    {
        string[] testCodes = {
            "PUSH 91",
            "PUSH 7",
            "DIV 2"
        };

        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;

        var left = new PrimaryExpASTNode(91);
        var right = new PrimaryExpASTNode(7);
        var node = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(node.Print(0), "91/7");
        checkVMReturnValueFromSubProgram(node, 91/7);
        checkGeneratedInstructionIsSame(testCodes, node);
    }


    public void test_ModFactorExpASTNode()
    {
        string[] testCodes = {
                "PUSH 91",
                "PUSH 13",
                "MOD 2"
            };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MOD;

        var left = new PrimaryExpASTNode(91);
        var right = new PrimaryExpASTNode(13);
        var node = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(node.Print(0), "91%13");
        checkVMReturnValueFromSubProgram(node, 91 % 13);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
}

