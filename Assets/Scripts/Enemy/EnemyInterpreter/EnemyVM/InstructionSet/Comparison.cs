using System;
public partial class EnemyVM
{
    private PrimitiveValue toInt(bool target)
    {
        return target ? PrimitiveValue.makeInt(1) : PrimitiveValue.makeInt(0);
    }
    private void Eq()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 == operand2));
    }
    private void Ne()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 != operand2));
    }
    private void Gt()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 > operand2));
    }
    private void Ge()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 >= operand2));
    }
    private void Lt()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 < operand2));
    }
    private void Le()
    {
       PrimitiveValue operand2 = PopFromStack();
       PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(toInt(operand1 <= operand2));
    }
}
