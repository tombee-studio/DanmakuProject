using System;
public partial class EnemyVM
{
    private int toInt(bool target)
    {
        return target ? 1 : 0;   
    }
    private void Eq()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand == 0));
    }
    private void Ne()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand != 0));
    }
    private void GT()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand > 0));
    }
    private void GE()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand >= 0));
    }
    private void LT()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand < 0));
    }
    private void LE()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand <= 0));
    }
}
