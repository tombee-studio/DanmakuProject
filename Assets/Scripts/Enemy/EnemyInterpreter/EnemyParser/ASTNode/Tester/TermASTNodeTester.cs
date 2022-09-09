using System;
using UnityEngine.Assertions;

public partial class EnemyASTNodeTester
{
    public void test_TermASTNode_print1()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MULTIPLY;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new NumberASTNode(1);
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(Term.Print(0), "1*1");
    }

    public void test_TermASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "PLUS 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.PLUS;
        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new NumberASTNode(1);
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, Term);
    }

    public void test_TermASTNode_print2()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;
        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new NumberASTNode(1);
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(left, arithmeticOperator1, right);
        Assert.AreEqual(Term.Print(0), "1/1");
    }

    public void test_TermASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "SUB 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;
        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new NumberASTNode(1);
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(left, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, Term);
    }
    public void test_TermASTNode_print4()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(null, arithmeticOperator1, right);
        Assert.AreEqual(Term.Print(0), "-1");
    }

    public void test_TermASTNode_compile4()
    {
        string[] testCodes = {
            "PUSH -1",
            "PUSH 1",
            "ADD 2"
        };
        var left = new NumberASTNode(-1);
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.PLUS;
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(null, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, Term);
    }

    public void test_TermASTNode5()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.NONE;
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(null, arithmeticOperator1, right);
        Assert.AreEqual(Term.Print(0), "1");
    }

    public void test_TermASTNode_compile5()
    {
        string[] testCodes = {
            "PUSH 1",
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.NONE;
        var right = new NumberASTNode(1);
        var Term = new TermASTNode(null, arithmeticOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, Term);
    }
}