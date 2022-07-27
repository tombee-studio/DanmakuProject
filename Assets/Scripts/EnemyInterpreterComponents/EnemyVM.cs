using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VMValueType = System.Int32;

public class EnemyVM
{
    public class EnemyVMException: Exception {
        public EnemyVMException(string message, EnemyVM vm) : base(message) {
            Debug.Log(vm.data.ToArray());
        }
    };

    public enum Mnemonic
    {
        PUSH,
        ADD,
        SUB,
        MUL,
        DIV
    };
    public struct Instruction
    {
        public Mnemonic mnemonic;
        public int argument;
    }
    private List<Instruction> instructions;
    private int programCounter;

    private Stack<VMValueType> data = new Stack<VMValueType>();
    private bool isContinue = false;
    private bool isExit = true;  
    private int retVal = 0;

    public bool IsContinute { get => isContinue; }
    public bool IsExit { get => isExit; }
    public int ReturnValue {
        get {
            if (data.Count == 0) {
                throw new EnemyVMException(
                    $"Stack Size must be more than {data.Count}",
                    this);
            }
            return data.Pop();
        }
    }
    //TODO: functions (何型?)

    public void run(){
        Instruction instruction = instructions[programCounter];
        switch (instruction.mnemonic)
        {
            case Mnemonic.PUSH: PUSH(instruction);
                break;
            case Mnemonic.ADD:  ADD();
                break;
            case Mnemonic.SUB:  SUB();
                break;
            case Mnemonic.MUL:  MUL();
                break;
            case Mnemonic.DIV:  DIV();
                break;
        }
        programCounter++;
    }
    public void PUSH(Instruction instruction) => data.Push(instruction.argument);
    public void ADD()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 + operand2);
    }
    public void SUB()
    {
        // スタックマシン{push ope1, push ope2, MUL} => push ope1 - ope2
        // スタックからPopされる、オペランドの順番はope2, ope1である。
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 - operand2);
    }
    public void MUL()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 * operand2);
    }
    public void DIV()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 / operand2);
    }
}
 