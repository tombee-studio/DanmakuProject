using System;
using UnityEngine.Assertions;

public partial class EnemyASTNodeTester
{
    public void test_TermExpASTNode_print1()
    {
        
        var arithmeticOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.PLUS);
        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new TermExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(term.Print(0), "1+1");
    }

    public void test_TermExpASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "ADD 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.PLUS;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new TermExpASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }

    public void test_TermExpASTNode_print2()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new TermExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(term.Print(0), "1-1");
    }

    public void test_TermExpASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "SUB 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new TermExpASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }
}