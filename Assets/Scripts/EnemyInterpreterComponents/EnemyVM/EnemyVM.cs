using System.Collections.Generic;

using VMValueType = System.Int32;

public partial class EnemyVM
{
    private static int MEMORY_SIZE = 256;

    private List<Instruction> instructionSeries = new List<Instruction>();
    private int programCounter;
    private int stackPointer = 0;
#pragma warning disable CS0414
    private int basePointer = 0;
#pragma warning restore CS0414
    private EnemyComponent enemyComponent;

    private VMValueType[] memory = new VMValueType[MEMORY_SIZE];
    private bool isContinue = true;
    private bool isExit = false;

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

    public EnemyVM(EnemyComponent enemyComponent)
    {
        this.enemyComponent = enemyComponent;
    }

    public void appendInstruction(Instruction instruction){
        instructionSeries.Add(instruction);
    }
    
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
            case Mnemonic.PUSH: Push(instruction); break;
            case Mnemonic.ADD:  Add(); break;
            case Mnemonic.SUB:  Sub(); break;
            case Mnemonic.MUL:  Mul(); break;
            case Mnemonic.DIV:  Div(); break;

            case Mnemonic.EQ: Eq(); break;
            case Mnemonic.NE: Ne(); break;

            case Mnemonic.GT: Gt(); break;
            case Mnemonic.GE: Ge(); break;
            case Mnemonic.LT: Lt(); break;
            case Mnemonic.LE: Le(); break;

            case Mnemonic.JMP: Jmp(instruction); break;
            case Mnemonic.JE: Je(instruction); break;
            case Mnemonic.JNE: Jne(instruction); break;



        }
        //TODO: programCounter++;を下に持っていく。
        //TODO: if分の終了条件を上に。
        programCounter++;
        if (instructionSeries.Count == programCounter) FinishProcess();


    }
    private void FinishProcess()
    {
        isExit = true;
        isContinue = false;
    }
    
}
 