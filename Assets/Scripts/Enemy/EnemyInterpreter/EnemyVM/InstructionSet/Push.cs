using System;
public partial class EnemyVM
{
    private void Push(Instruction instruction)
    {
        PushIntoStack(instruction.argument);
    }
}
