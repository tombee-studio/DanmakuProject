using System;
public partial class EnemyVM
{
    void Call(Instruction instruction)
    {
        EnemyFunctionFactory
            .GetInstance()
            .Call(instruction.argument, enemyComponent, this);
    }
}
