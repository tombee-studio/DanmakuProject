using UnityEngine.Assertions;

public partial class EnemyVMTester
{
    public void test_JMP()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.JMP, 4)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 1)
        );
        while (!IsExit) run();
        Assert.AreEqual(3, vm.ReturnValue);
    }

    public void test_JE1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.JE, 5)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 1)
        );
        while (!IsExit) run();
        Assert.AreEqual(2, vm.ReturnValue);
    }

    public void test_JE2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.JE, 5)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(2, vm.ReturnValue);
    }

    public void test_JNE1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.JNE, 5)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 1)
        );
        while (!IsExit) run();
        Assert.AreEqual(3, vm.ReturnValue);
    }

    public void test_JNE2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.JNE, 5)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 1)
        );
        while (!IsExit) run();
        Assert.AreEqual(3, vm.ReturnValue);
    }
}
