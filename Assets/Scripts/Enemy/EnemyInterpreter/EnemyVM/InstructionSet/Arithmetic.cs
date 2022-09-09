using System;
public partial class EnemyVM
{

    private void Add()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 + operand2);
    }

    private void Sub()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 - operand2);
    }

    private void Mul()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 * operand2);
    }

    private void Div()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 / operand2);
    }

    private void Mod()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 % operand2);
    }
}
