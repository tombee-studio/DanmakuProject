using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_BreakStASTNode()
    {
        string[] testCodes = {
            "BREAK 0",
        };
        var node = new BreakStASTNode();
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(node.Print(0), "break\n");
    }
}

