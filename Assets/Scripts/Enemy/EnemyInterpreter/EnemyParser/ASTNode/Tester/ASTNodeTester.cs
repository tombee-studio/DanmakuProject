using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester : Tester
{
    protected override Tester cloneThisObject()
    {
        return new EnemyASTNodeTester();
    }
    private class ExpectedExceptionNotThrownException : Exception
    {
        public ExpectedExceptionNotThrownException() : base() { }
        public ExpectedExceptionNotThrownException(string message) : base(message) { }
        public ExpectedExceptionNotThrownException(string message, Exception innerException) : base(message, innerException) { }
    }

    private string GetTabs(int tabCount)
    {
        string tab = "\t";
        string tabs = "";
        for (int i = 0; i < tabCount; i++)
        {
            tabs += tab;
        }
        return tabs;
    }
    private void checkGeneratedInstructionIsSame(string[] testCodes, ASTNode node)
    {
        node.Compile(new Dictionary<string, int>())
            .Zip(testCodes, (i1, i2) => (i1, i2)).ToList()
            .ForEach(instruction =>
                {
                    var (i1, i2) = instruction;
                    Assert.AreEqual(i2, $"{i1}");
                }
            );
    }
    private void checkIsPrintScript(string[] scriptCodes, ASTNode node)
    {
        node.Print(0)
            .Split("\n")
            .Zip(scriptCodes, (i1, i2) => (i1, i2)).ToList()
            .ForEach(code =>
                {
                    var (i1, i2) = code;
                    Assert.AreEqual(i2, i1);
                }
            );
    }
    void checkVMReturnValueFromSubProgram(ASTNode node, PrimitiveValue value)
    {
        var vm = new EnemyVM(null);
        var vTable = new Dictionary<string, int>();
        var subProgram = node.Compile(vTable);
        var declarations = new List<EnemyVM.Instruction>();
        for (int i = 0; i < vTable.Count; i++)
        {
            declarations.Add(new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, int.MaxValue));
        }
        var program = declarations
            .Concat(subProgram)
            .Select((instruction, line) =>
            {
                switch (instruction.mnemonic)
                {
                    case EnemyVM.Mnemonic.JMP:
                    case EnemyVM.Mnemonic.JE:
                    case EnemyVM.Mnemonic.JNE:
                        instruction.argument += line;
                        return instruction;
                    default:
                        return instruction;
                }
            }).ToList();
        program
            .ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit)
        {
            vm.run();
        }
        Assert.AreEqual(value, vm.ReturnValue);
    }
    private void checkVMReturnValue(ASTNode node, PrimitiveValue value)
    {
        var vm = new EnemyVM(null);
        var program = node.Compile(new Dictionary<string, int>()).ToList();
        program.ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit) { vm.run(); }
        Assert.AreEqual(value, vm.ReturnValue);
    }
}

