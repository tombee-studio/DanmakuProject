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
                }
            );
    }
    void checkVMReturnValueFromSubProgram(ASTNode node, PrimitiveValue value)
    {
        var vm = new EnemyVM(null);
        var compiled = node.Compile(new Dictionary<string, int>());
        var program = compiled.Select((instruction, line) =>
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
        program.ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit) { vm.run(); }
        Assert.AreEqual(vm.ReturnValue, value);
    }
    private void checkVMReturnValue(ASTNode node, PrimitiveValue value)
    {
        var vm = new EnemyVM(null);
        var compiled = node.Compile(new Dictionary<string, int>()).ToList();
        var program = compiled;
        program.ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit) { vm.run(); }
        Assert.AreEqual(vm.ReturnValue, value);
    }

}

