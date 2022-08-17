using System;
public partial class EnemyVM
{

    private void Add()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(operand1 + operand2);
    }

    private void Sub()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(operand1 - operand2);
    }

    private void Mul()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(operand1 * operand2);
    }

    private void Div()
    {
        int operand1 = PopFromStack();
        int operand2 = PopFromStack();
        PushIntoStack(operand1 / operand2);
    }
}
