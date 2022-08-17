using System;
public partial class EnemyVM
{
    private int toInt(bool target)
    {
        return target ? 1 : 0;   
    }
    private void Eq()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 == operand2));
    }
    private void Ne()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 != operand2));
    }
    private void Gt()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 > operand2));
    }
    private void Ge()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 >= operand2));
    }
    private void Lt()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 < operand2));
    }
    private void Le()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 <= operand2));
    }
}
