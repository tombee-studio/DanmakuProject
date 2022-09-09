using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    public void test_EqualityExpASTNode_print1()
    {
        var relationOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.EQUAL);
        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new EqualityExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1==1");
    }

    public void test_EqualityExpASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "EQ 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.EQUAL;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new EqualityExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }

    public void test_EqualityExpASTNode_print2()
    {
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.NOT;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new EqualityExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "1!=1");
    }

    public void test_EqualityExpASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "NE 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.NOT;

        var left = new PrimaryExpASTNode(1);
        var right = new PrimaryExpASTNode(1);
        var term = new EqualityExpASTNode(left, relationOperator1, right);
        checkGeneratedInstructionIsSame(testCodes, term);
    }

}

