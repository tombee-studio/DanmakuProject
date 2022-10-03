using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    public void test_LogicalExpASTNode_print1()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "AND 0"
        };
        var logicalOperator = ScriptToken.GenerateToken("", ScriptToken.Type.AND);
        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new LogicalExpASTNode(left, logicalOperator, right);

        Assert.AreEqual("6and4", node.Print(0));
        checkVMReturnValueFromSubProgram(node, 1);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_LogicalANDOutput(){
        var logicalOperator = ScriptToken.GenerateToken("", ScriptToken.Type.AND);
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(0), logicalOperator, new PrimaryExpASTNode(0)),
            0
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(0), logicalOperator, new PrimaryExpASTNode(1)),
            0
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(1), logicalOperator, new PrimaryExpASTNode(1)),
            1
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(1), logicalOperator, new PrimaryExpASTNode(0)),
            0
        );
    }

    public void test_LogicalExpASTNode_print2()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "OR 0"
        };
        var logicalOperator = new ScriptToken();
        logicalOperator.type = ScriptToken.Type.OR;

        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new LogicalExpASTNode(left, logicalOperator, right);

        Assert.AreEqual("6or4", node.Print(0));
        checkVMReturnValueFromSubProgram(node, 1);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_LogicalOROutput(){
        var logicalOperator = ScriptToken.GenerateToken("", ScriptToken.Type.OR);
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(0), logicalOperator, new PrimaryExpASTNode(0)),
            0
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(0), logicalOperator, new PrimaryExpASTNode(1)),
            1
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(1), logicalOperator, new PrimaryExpASTNode(1)),
            1
        );
        checkVMReturnValueFromSubProgram(new LogicalExpASTNode(
            new PrimaryExpASTNode(1), logicalOperator, new PrimaryExpASTNode(0)),
            1
        );
    }
}

