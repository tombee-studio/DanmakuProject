using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VMValueType = System.Int32;

public class EnemyVM
{
    public class EnemyVMException: Exception {
        public EnemyVMException(string message, EnemyVM vm) : base(message) {
            Debug.Log(vm.memory);
        }
    };

    public struct Instruction
    {
        public Mnemonic mnemonic;
        public int argument;
        public Instruction(Mnemonic mnemonic, int argument)
        {
            this.mnemonic = mnemonic;
            this.argument = argument;
        }
    }

    public enum Mnemonic
    {
        PUSH,
        ADD,
        SUB,
        MUL,
        DIV
    };

    private static int MEMORY_SIZE = 256;

    private List<Instruction> instructionSeries = new List<Instruction>();
    private int programCounter;
    private int stackPointer = 0;
    private int basePointer = 0;

    private VMValueType[] memory = new VMValueType[MEMORY_SIZE];
    private bool isContinue = true;
    private bool isExit = false;  
    private int retVal = 0;

    public bool IsContinute { get => isContinue; }
    public bool IsExit { get => isExit; }
    public int ReturnValue {
        get {
            if (memory.Length == 0) {
                throw new EnemyVMException(
                    $"Memory Size must be more than {memory.Length}",
                    this);
            }
            return memory[stackPointer];
        }
    }
    public void appendInstruction(Instruction instruction){
        instructionSeries.Add(instruction);
    }
    //TODO: functions (何型?)

    private VMValueType Peek() => memory[stackPointer];
    private VMValueType PopFromStack()
    {
        stackPointer--;
        return memory[stackPointer + 1];
    }
    private void PushIntoStack(VMValueType pushedValue)
    {
        stackPointer++;
        memory[stackPointer] = pushedValue;
    }

    public void run(){
        Instruction instruction = instructionSeries[programCounter];
        switch (instruction.mnemonic)
        {
            case Mnemonic.PUSH: Push(instruction);
                break;
            case Mnemonic.ADD:  Add();
                break;
            case Mnemonic.SUB:  Sub();
                break;
            case Mnemonic.MUL:  Mul();
                break;
            case Mnemonic.DIV:  Div();
                break;
        }
        programCounter++;
        if (instructionSeries.Count == programCounter) FinishProcess();


    }
    private void FinishProcess()
    {
        isExit = true;
        isContinue = false;
    }

    //TODO: 命名のルールにそって書き換える。
    
    private void Push(Instruction instruction) {
        PushIntoStack(instruction.argument);
    }

    private void Add()
    {
        int operand2 = PopFromStack();
        int operand1 = PopFromStack();
        PushIntoStack(operand1 + operand2);
    }

    private void Sub()
    {
        // スタックマシン{push ope1, push ope2, MUL} => push ope1 - ope2
        // スタックからPopされる、オペランドの順番はope2, ope1である。
        int operand2 = PopFromStack();
        int operand1 = PopFromStack();
        PushIntoStack(operand1 - operand2);
    }

    private void Mul()
    {
        int operand2 = PopFromStack();
        int operand1 = PopFromStack();
        PushIntoStack(operand1 * operand2);
    }

    private void Div()
    {
        int operand2 = PopFromStack();
        int operand1 = PopFromStack();
        PushIntoStack(operand1 / operand2);
    }
}
 