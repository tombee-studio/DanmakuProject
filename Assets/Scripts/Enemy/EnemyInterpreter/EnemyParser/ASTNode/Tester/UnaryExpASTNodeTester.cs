using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_NoneSignUnaryExpCompile()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}"
        };
        UnaryExpASTNode node = new PrimaryExpASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    void test_NoneSignUnaryExpPrint()
    {
        int value = 42;
        UnaryExpASTNode node = new PrimaryExpASTNode(value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    void test_NegativeSignUnaryExpCompile()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}",
            $"PUSH {-1}",
            $"MUL {0}"
        };
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.SUB), new PrimaryExpASTNode(value));
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    void test_NegativeSignUnaryExpPrint()
    {
        int value = 42;
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.SUB), new PrimaryExpASTNode(value));
        Assert.AreEqual(node.Print(0), $"-{value}");
    }
    void test_PositiveSignUnaryExpCompile()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}",
        };
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.PLUS), new PrimaryExpASTNode(value));
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    void test_PositiveSignUnaryExpPrint()
    {
        int value = 42;
        UnaryExpASTNode node = new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.PLUS), new PrimaryExpASTNode(value));
        Assert.AreEqual(node.Print(0), $"+{value}");
    }
}

