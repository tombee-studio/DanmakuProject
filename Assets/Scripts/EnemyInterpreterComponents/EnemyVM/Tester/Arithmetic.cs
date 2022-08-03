using UnityEngine.Assertions;

public partial class EnemyVMTester
{
    public void test_ADD()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 9)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(11, vm.ReturnValue);
    }
    public void test_SUB()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 9)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.SUB, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(7, vm.ReturnValue);
    }
    public void test_MUL()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 9)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(18, vm.ReturnValue);
    }
    public void test_DIV1()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 18)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(9, vm.ReturnValue);
    }
    /** 整数除算がなされるかテスト */
    public void test_DIV2()
    {
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 7)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(3, vm.ReturnValue);
    }
}
