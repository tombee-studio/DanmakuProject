﻿using System;
public partial class EnemyVM
{
    private void Jmp(Instruction instruction)
    {
        programCounter = instruction.argument;
    }
    private void Je(Instruction instruction)
    {
        if (PopFromStack() == 0) programCounter = instruction.argument;
    }
    private void Jne(Instruction instruction)
    {
        if (PopFromStack() != 0) programCounter = instruction.argument;
    }
}
