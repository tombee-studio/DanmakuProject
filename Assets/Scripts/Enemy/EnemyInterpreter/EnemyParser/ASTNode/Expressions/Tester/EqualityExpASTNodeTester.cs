using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    public void test_EqualityExpASTNode_print1()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "EQ 2"
        };
        var relationOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.EQUAL);
        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new EqualityExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(node.Print(0), "6==4");
        checkVMReturnValueFromSubProgram(node, 6 == 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, node);
    }

    public void test_EqualityExpASTNode_print2()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "NE 2"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.NOT;

        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new EqualityExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(node.Print(0), "6!=4");
        checkVMReturnValueFromSubProgram(node, 6 != 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
}

