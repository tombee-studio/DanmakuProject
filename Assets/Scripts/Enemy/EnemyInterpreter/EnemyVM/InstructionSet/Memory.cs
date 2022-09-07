using System;
public partial class EnemyVM
{
    void Load()
    {
        var address = PopFromStack();
        PushIntoStack(memory[address]);
    }
    void Store()
    {
        var address = PopFromStack();
        var value = PopFromStack();
        memory[address] = value;
    }
}
