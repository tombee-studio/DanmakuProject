using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_DeclarationStASTNode()
    {
        string[] testCodes = {};
        var node = new DeclarationStASTNode(PrimitiveValue.Type.INT, "i");
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual("int i\n", node.Print(0));
    }
}

