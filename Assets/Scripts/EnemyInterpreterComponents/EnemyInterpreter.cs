using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;


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

    public void test_run() {
        var tmp_interpreter = new EnemyInterpreter();
        BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        flag |= BindingFlags.NonPublic;
        MethodInfo[] methods = tmp_interpreter.GetType().GetMethods(flag);
        foreach (MethodInfo method in methods)
        {
            if (Regex.IsMatch(method.Name, "test_run_"))
            {
                object[] parametersArray = new object[] {};
                method.Invoke(tmp_interpreter, parametersArray);
            }
        }
    }

    public void test_run_ADD()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH,  2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH,  9)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD,   0)
        );
        while (!IsExit) run();
        Assert.AreEqual(11, vm.ReturnValue);
    }
    public void test_run_SUB()
    {
        //TODO: 命令を追加
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
    public void test_run_MUL()
    {
        //TODO: 命令を追加
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
    public void test_run_DIV()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 18)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 9)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.DIV, 0)
        );
        while (!IsExit) run();
        Assert.AreEqual(2, vm.ReturnValue);
    }

    public void test_run_flag1()
    {
        //TODO: 命令を追加
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
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag2()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.EQ, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag3()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.NE, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag4()
    {
        //TODO: 命令を追加
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
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag5()
    {
        //TODO: 命令を追加
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
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag6()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag7()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LT, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag8()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic. GT, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag9()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GT, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag10()
    {
        //TODO: 命令を追加
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
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag11()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag12()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.LE, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag13()
    {
        //TODO: 命令を追加
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
        Assert.IsTrue(vm.ReturnValue == 1);
    }

    public void test_run_flag14()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 0);
    }

    public void test_run_flag15()
    {
        //TODO: 命令を追加
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 3)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 2)
        );
        vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.GE, 0)
        );
        while (!IsExit) run();
        Assert.IsTrue(vm.ReturnValue == 1);
    }
}
