using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;
public partial class EnemyASTNodeTester
{
    public void test_intNumber()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}"
        };
        var node = new PrimaryExpASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    public void test_floatNumber()
    {
        float value = 3.14f;
        string[] testCodes = {
            $"PUSH {value}"
        };
        var node = new PrimaryExpASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    public void test_Variable()
    {
        string id = "position";
        int address = 0;
        string[] testCodes = {
            $"PUSH {address}"
        };
        var astNode = new PrimaryExpASTNode(id);
        var vTable = new Dictionary<string, int>();
        vTable.Add(id, address);
        foreach (var instruction in astNode.Compile(vTable)
            .Zip(testCodes, (i1, i2) => (i1, i2)))
        {
            var (i1, i2) = instruction;
            Assert.AreEqual($"{i1}", i2);
        }
        var node = new PrimaryExpASTNode(id);
        Assert.AreEqual(node.Print(0), $"{id}");
    }
    public void test_ParenExp()
    {
        int value = 42;
        ExpASTNode exp = new PrimaryExpASTNode(value);
        string[] testCodes = {
            $"PUSH {value}"
        };
        var node = new PrimaryExpASTNode(exp);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, value);
        Assert.AreEqual(node.Print(0), $"({value})");
    }
}

