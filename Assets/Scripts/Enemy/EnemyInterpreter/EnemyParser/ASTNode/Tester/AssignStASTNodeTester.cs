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
        var node = new AssignStASTNode(
            id,
            new PrimaryExpASTNode(value)
        );
        Assert.AreEqual(node.Print(0), $"{id} = {value}\n");
        try
        {
            node.Compile(new Dictionary<string, int>());
            throw new ExpectedExceptionNotThrownException();
        }
        catch (KeyNotFoundException e)
        {
            // DoNothing(); because expected exception thrown.
        }
    }
    // TODO: 複数行のテストは block の方でやる
    // 　-> 変数の load, 2 変数以上の宣言が該当
}

