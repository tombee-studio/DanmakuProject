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
        PushIntoStack(toInt(operand != 0));
    }
    private void Ne()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand == 0));
    }
    private void Gt()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand > 0));
    }
    private void Ge()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand >= 0));
    }
    private void Lt()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand < 0));
    }
    private void Le()
    {
        int operand = PopFromStack();
        PushIntoStack(toInt(operand <= 0));
    }
}
