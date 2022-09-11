using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    public void test_GtRelationalExpASTNode()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "GT 0"
        };
        var relationOperator = ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN);
        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new RelationalExpASTNode(left, relationOperator, right);

        Assert.AreEqual(node.Print(0), "6>4");
        checkVMReturnValueFromSubProgram(node, 6 > 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_GeRelationalExpASTNode()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "GE 0"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.GREATER_EQUAL;

        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(node.Print(0), "6>=4");
        checkVMReturnValueFromSubProgram(node, 6 >= 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_LtRelationalExpASTNode()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "LT 0"
        };
        var relationOperator1 = ScriptToken.GenerateToken("", ScriptToken.Type.LESS_THAN);
        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var node = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(node.Print(0), "6<4");
        checkVMReturnValueFromSubProgram(node, 6 < 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_LeRelationalExpASTNode()
    {
        string[] testCodes = {
            "PUSH 6",
            "PUSH 4",
            "LE 0"
        };
        var relationOperator1 = new ScriptToken();
        relationOperator1.type = ScriptToken.Type.LESS_EQUAL;

        var left = new PrimaryExpASTNode(6);
        var right = new PrimaryExpASTNode(4);
        var term = new RelationalExpASTNode(left, relationOperator1, right);

        Assert.AreEqual(term.Print(0), "6<=4");
        checkVMReturnValueFromSubProgram(term, 6 <= 4 ? 1 : 0);
        checkGeneratedInstructionIsSame(testCodes, term);
    }
}

