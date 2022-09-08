using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public partial class EnemyASTNodeTester{
    public void test_intNumberCompile(){
        int value = 42;
        NumberASTNode node = new NumberASTNode(value);
        List<EnemyVM.Instruction> instructions = node.Compile(null);
        Assert.AreEqual(instructions.Count, 1);
        Assert.AreEqual(instructions[0].mnemonic, EnemyVM.Mnemonic.PUSH);
        Assert.AreEqual(instructions[0].argument, (PrimitiveValue)value);
    }
    public void test_intNumberPrint(){
        int value = 42;
        NumberASTNode node = new NumberASTNode(value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
    public void test_floatNumberCompile(){
        float value = 3.14f;
        NumberASTNode node = new NumberASTNode(value);
        List<EnemyVM.Instruction> instructions = node.Compile(null);
        Assert.AreEqual(instructions.Count, 1);
        Assert.AreEqual(instructions[0].mnemonic, EnemyVM.Mnemonic.PUSH);
        Assert.AreEqual(instructions[0].argument, (PrimitiveValue)value);
    }
    public void test_floatNumberPrint(){
        float value = 3.14f;
        NumberASTNode node = new NumberASTNode(value);
        Assert.AreEqual(node.Print(0), $"{value}");
    }
}
