using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VMValueType = System.Int32;

public partial class EnemyVM
{
    public class EnemyVMException: Exception {
        public EnemyVMException(string message, EnemyVM vm) : base(message) {
            Debug.Log(vm.data.ToArray());
        }
    };
    private List<Instruction> instructionSeries;
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
    public void appendInstruction(Instruction instruction){
        instructionSeries.Add(instruction);
    }
    //TODO: functions (何型?)

    public void run(){
        Instruction instruction = instructionSeries[programCounter];
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
        if (instructionSeries.Count == programCounter) finishProcess();


    }
    private void finishProcess()
    {
        isExit = true;
        isContinue = false;
        retVal = data.Pop();
    }
    private void PUSH(Instruction instruction) => data.Push(instruction.argument);
    private void ADD()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 + operand2);
    }
    private void SUB()
    {
        // スタックマシン{push ope1, push ope2, MUL} => push ope1 - ope2
        // スタックからPopされる、オペランドの順番はope2, ope1である。
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 - operand2);
    }
    private void MUL()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 * operand2);
    }
    private void DIV()
    {
        int operand2 = data.Pop();
        int operand1 = data.Pop();
        data.Push(operand1 / operand2);
    }
}
 