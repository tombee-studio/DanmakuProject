using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;
public partial class EnemyASTNodeTester
{
    public void test_intNumberCompile()
    {
        int value = 42;
        string[] testCodes = {
            $"PUSH {value}"
        };
        PrimaryExpASTNode node = new PrimaryExpASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_intNumberPrint()
    {
        int value = 42;
        PrimaryExpASTNode node = new PrimaryExpASTNode(value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    public void test_floatNumberCompile()
    {
        float value = 3.14f;
        string[] testCodes = {
            $"PUSH {value}"
        };
        PrimaryExpASTNode node = new PrimaryExpASTNode(value);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_floatNumberPrint()
    {
        float value = 3.14f;
        PrimaryExpASTNode node = new PrimaryExpASTNode(value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    public void test_VariableCompile()
    {
        string id = "position";
        int address = 0;
        string[] testCodes = {
            $"PUSH {address}"
        };
        PrimaryExpASTNode astNode = new PrimaryExpASTNode(id);
        var vTable = new Dictionary<string, int>();
        vTable.Add(id, address);
        foreach (var instruction in astNode.Compile(vTable)
            .Zip(testCodes, (i1, i2) => (i1, i2)))
        {
            var (i1, i2) = instruction;
            Assert.AreEqual($"{i1}", i2);
        }
    }
    public void test_VariablePrint()
    {
        string id = "position";
        PrimaryExpASTNode node = new PrimaryExpASTNode(id);
        Assert.AreEqual(node.Print(0), $"{id}");
    }
    public void test_ParenExpCompile()
    {
        int value = 42;
        ExpASTNode exp = new PrimaryExpASTNode(value);
        string[] testCodes = {
            $"PUSH {value}"
        };
        var node = new PrimaryExpASTNode(exp);
        checkGeneratedInstructionIsSame(testCodes, node);
    }
    public void test_ParenExpPrint()
    {
        int value = 42;
        ExpASTNode exp = new PrimaryExpASTNode(value);
        var node = new PrimaryExpASTNode(exp);
        Assert.AreEqual(node.Print(0), $"({value})");
    }
}

