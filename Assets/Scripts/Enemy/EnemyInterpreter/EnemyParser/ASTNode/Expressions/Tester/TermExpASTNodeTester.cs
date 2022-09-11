using System;
using UnityEngine.Assertions;

public partial class EnemyASTNodeTester
{
    public void test_AddTermExpASTNode()
    {
        string[] testCodes = {
            "PUSH 13",
            "PUSH 7",
            "ADD 2"
        };        
        var arithmeticOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.PLUS);
        var left = new PrimaryExpASTNode(13);
        var right = new PrimaryExpASTNode(7);
        var node = new TermExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(node.Print(0), "13+7");
        checkVMReturnValueFromSubProgram(node, 13+7);
        checkGeneratedInstructionIsSame(testCodes, node);
    }

    public void test_SubTermExpASTNode()
    {
        string[] testCodes = {
            "PUSH 42",
            "PUSH 20",
            "SUB 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;

        var left = new PrimaryExpASTNode(42);
        var right = new PrimaryExpASTNode(20);
        var node = new TermExpASTNode(left, arithmeticOperator1, right);

        Assert.AreEqual(node.Print(0), "42-20");
        checkVMReturnValueFromSubProgram(node, 42-20);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
}