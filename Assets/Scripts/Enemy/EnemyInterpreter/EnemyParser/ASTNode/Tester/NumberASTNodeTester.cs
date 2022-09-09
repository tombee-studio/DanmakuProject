using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using UnityEngine;

public partial class EnemyASTNodeTester
{
    public void test_intNumberCompile()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}"
        };
        NumberASTNode node = new NumberASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_intNumberPrint()
    {
        int value = 42;
        int tab = 2;
        NumberASTNode node = new NumberASTNode(value);
        Assert.AreEqual(node.Print(tab), GetTabs(tab) + $"{value}");
    }
    public void test_floatNumberCompile()
    {
        float value = 3.14f;
        string[] testCodes = {
            $"PUSH {value}"
        };
        NumberASTNode node = new NumberASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_floatNumberPrint()
    {
        float value = 3.14f;
        int tab = 3;
        NumberASTNode node = new NumberASTNode(value);
        Assert.AreEqual(node.Print(tab), GetTabs(tab) + $"{value}");
    }
}
