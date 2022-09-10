using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_StoreAssignStASTNodeTester()
    {
        var id = "x";
        var value = 42;
        string[] testCodes = {
            $"PUSH {value}",
            $"PUSH {0}",
            $"STORE 0",
        };
        var node = new AssignStASTNode(
            id,
            new PrimaryExpASTNode(value)
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(node.Print(0), $"{id} = {value}\n");
    }
    // TODO: 複数行のテストは block の方でやる
    // 　-> 変数の load, 2 変数以上の宣言が該当
}

