using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    public void test_RelationalExpASTNode_print1()
    {
        var relationOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN);
        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1>1");
    }

    public void test_RelationalExpASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "GT 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.GREATER_THAN;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }

    public void test_RelationalExpASTNode_print2()
    {
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.GREATER_EQUAL;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1>=1");
    }

    public void test_RelationalExpASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "GE 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.GREATER_EQUAL;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }
    public void test_RelationalExpASTNode_print3()
    {
        var relationOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.LESS_THAN);
        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1<1");
    }

    public void test_RelationalExpASTNode_compile3()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "LT 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.LESS_THAN;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }

    public void test_RelationalExpASTNode_print4()
    {
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.LESS_EQUAL;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1<=1");
    }

    public void test_RelationalExpASTNode_compile4()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "LE 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.LESS_EQUAL;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new RelationalExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }
}

