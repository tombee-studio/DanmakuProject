using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public partial class EnemyASTNodeTester
{
    public void test_FactorExpASTNode_print1()
    {
        
        var arithmeticOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY);
        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(factor.Print(0), "1*1");
    }

    public void test_FactorExpASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "MUL 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MULTIPLY;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, factor);
    }

    public void test_FactorExpASTNode_print2()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(factor.Print(0), "1/1");
    }

    public void test_FactorExpASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "DIV 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, factor);
    }

    public void test_FactorExpASTNode_print3()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MOD;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(factor.Print(0), "1%1");
    }

    public void test_FactorExpASTNode_compile3()
    {
        string[] testCodes = {
                "PUSH 1",
                "PUSH 1",
                "MOD 2"
            };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MOD;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var factor = new FactorExpASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, factor);
    }
}

