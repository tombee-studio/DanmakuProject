using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_NoneSignUnaryExp()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}"
        };
        UnaryExpASTNode node = new PrimaryExpASTNode(value);
        checkVMReturnValueFromSubProgram(node, value);
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    void test_NegativeSignUnaryExp()
    {
        int value = 91;
        string[] testCodes = {
            $"PUSH {value}",
            $"PUSH {-1}",
            $"MUL {2}"
        };
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.SUB), new PrimaryExpASTNode(value));
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, -value);
        Assert.AreEqual(node.Print(0), $"-{value}");
    }
    void test_PositiveSignUnaryExp()
    {
        int value = 57;
        string[] testCodes = {
            $"PUSH {value}",
        };
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.PLUS), new PrimaryExpASTNode(value));
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, value);
        Assert.AreEqual(node.Print(0), $"+{value}");
    }
}

