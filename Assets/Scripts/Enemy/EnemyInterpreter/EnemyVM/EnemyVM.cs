using System;
using System.Collections.Generic;
using System.Linq;

using VMValueType = PrimitiveValue;

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
    public PrimitiveValue ReturnValue
    {
        get
        {
            if (memory.Length == 0)
            {
                throw new EnemyVMException(
                    $"Memory Size must be more than {memory.Length}",
                    this);
            }
            return memory[stackPointer];
        }
    }
    public VMValueType[] StackTrace
    {
        get
        {
            if (memory.Length == 0)
            {
                throw new EnemyVMException(
                    $"Memory Size must be more than {memory.Length}",
                    this);
            }
            var memoryList = memory.ToList();
            int maxIndex = memoryList.FindLastIndex(e =>
            {
                switch (e.type)
                {
                    case PrimitiveValue.Type.INT:
                        return e != 0;
                    case PrimitiveValue.Type.FLOAT:
                        return e != 0f;
                    default:
                        throw new NotImplementedException($"Unexpected type {e.type} reserved");
                }
            });
            return memoryList.GetRange(0, maxIndex + 1).ToArray();
        }
    }

    public List<string> InstructionsTrace
    {
        get
        {
            return instructionSeries.Select((e, i) =>
            {
                string msg;
                if (i == programCounter) {
                    msg = "--> ";
                }
                else{
                    msg = "    ";
                }
                msg += $"{i}: {e}";
                return msg;
            }).ToList();
        }
    }


    public EnemyVM(EnemyComponent enemyComponent)
    {
        this.enemyComponent = enemyComponent;
    }

    public void appendInstruction(Instruction instruction)
    {
        instructionSeries.Add(instruction);
    }

    private VMValueType Peek() => memory[stackPointer];

    public VMValueType PopFromStack()
    {
        stackPointer--;
        return memory[stackPointer + 1];
    }
    public void PushIntoStack(VMValueType pushedValue)
    {
        stackPointer++;
        memory[stackPointer] = pushedValue;
    }

    public void run()
    {
        if (programCounter >= instructionSeries.Count)
        {
            FinishProcess();
            return;
        }
        Instruction instruction = instructionSeries[programCounter];
        switch (instruction.mnemonic)
        {
            case Mnemonic.PUSH: Push(instruction); break;
            case Mnemonic.ADD: Add(); break;
            case Mnemonic.SUB: Sub(); break;
            case Mnemonic.MUL: Mul(); break;
            case Mnemonic.DIV: Div(); break;
            case Mnemonic.MOD: Mod(); break;

            case Mnemonic.EQ: Eq(); break;
            case Mnemonic.NE: Ne(); break;

            case Mnemonic.GT: Gt(); break;
            case Mnemonic.GE: Ge(); break;
            case Mnemonic.LT: Lt(); break;
            case Mnemonic.LE: Le(); break;

            case Mnemonic.JMP: Jmp(instruction); break;
            case Mnemonic.JE: Je(instruction); break;
            case Mnemonic.JNE: Jne(instruction); break;
            case Mnemonic.BREAK: throw new EnemyVMException("ダミー命令が呼ばれました. BREAK 命令はループ末端へのジャンプ命令へ置換してください.", this);

            case Mnemonic.CALL: Call(instruction); break;

            case Mnemonic.LOAD: Load(); break;
            case Mnemonic.STORE: Store(); break;

        }
        programCounter++;
    }
    private void FinishProcess()
    {
        isExit = true;
        isContinue = false;
    }

}
