using UnityEngine.Assertions;

public partial class EnemyVMTester
{

    public void test_EQ1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }

    public void test_EQ2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }


    public void test_NE1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.NE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }

    public void test_NE2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.NE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }


    public void test_LT1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }

    public void test_LT2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }


    public void test_GT1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GT, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }

    public void test_GT2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GT, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }


    public void test_LE1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }

    public void test_LE2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }

    public void test_LE3()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }


    public void test_GE1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }

    public void test_GE2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(1, vm.ReturnValue);
    }

    public void test_GE3()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(0, vm.ReturnValue);
    }
}
