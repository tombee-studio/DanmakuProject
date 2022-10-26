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
#pragma warning disable CS0168  // e is not used but required to check if expected error is thrown
        catch (KeyNotFoundException e)
#pragma warning restore CS0168
        {
            // DoNothing(); because expected exception thrown.
        }
    }
}

