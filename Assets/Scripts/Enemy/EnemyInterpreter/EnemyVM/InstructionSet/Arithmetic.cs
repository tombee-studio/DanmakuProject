using System;
public partial class EnemyVM
{

    private void Add()
    {
        PrimitiveValue operand1 = PopFromStack();
        PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(operand1 + operand2);
    }

    private void Sub()
    {
        PrimitiveValue operand1 = PopFromStack();
        PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(operand1 - operand2);
    }

    private void Mul()
    {
        PrimitiveValue operand1 = PopFromStack();
        PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(operand1 * operand2);
    }

    private void Div()
    {
        PrimitiveValue operand1 = PopFromStack();
        PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(operand1 / operand2);
    }

    private void Mod()
    {
        PrimitiveValue operand1 = PopFromStack();
        PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(operand1 % operand2);
    }
}
