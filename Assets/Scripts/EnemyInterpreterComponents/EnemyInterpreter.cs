using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyInterpreter
{
    #nullable enable
    EnemyCompiler? _compiler=null;
    public EnemyCompiler compiler {get => _compiler ??= new EnemyCompiler();}

    EnemyVM? _vm;
    public EnemyVM vm {get => _vm ??= new EnemyVM(); }
    public bool IsContinue { get =>  vm.IsContinute; }
    public bool IsExit {  get => vm.IsExit; }
    public int ReturnValue { get => vm.ReturnValue; }

    public void run(){
        vm.run();
    }
    public void test_run()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH,  2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH,  9)
        );
        // stack: {2, 9}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD,   0)
        );
        // stack: {11}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        // stack: {11, 3}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.SUB, 0)
        );
        // stack: {8}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        // stack: {8, 2, 2}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0)
        );
        // stack: {8, 4}
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 0)
        );
        // stack: {2}
        while (!IsExit) run();
    }
}
