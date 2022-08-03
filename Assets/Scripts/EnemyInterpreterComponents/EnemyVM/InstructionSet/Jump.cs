using System;
public partial class EnemyVM
{
    private void Jmp(Instruction instruction)
    {
        programCounter = instruction.argument;
    }
    private void Je(Instruction instruction)
    {
        if (Peek() != 0) programCounter = instruction.argument;
    }
    private void Jne(Instruction instruction)
    {
        if (Peek() == 0) programCounter = instruction.argument;
    }
}
