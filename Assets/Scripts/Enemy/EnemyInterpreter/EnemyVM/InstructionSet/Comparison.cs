using System;
public partial class EnemyVM
{
    private PrimitiveValue toInt(bool target)
    {
        return target ? PrimitiveValue.makeInt(1) : PrimitiveValue.makeInt(0);
    }
    private void Eq()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 == operand2));
    }
    private void Ne()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 != operand2));
    }
    private void Gt()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 > operand2));
    }
    private void Ge()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 >= operand2));
    }
    private void Lt()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 < operand2));
    }
    private void Le()
    {
       PrimitiveValue operand1 = PopFromStack();
       PrimitiveValue operand2 = PopFromStack();
        PushIntoStack(toInt(operand1 <= operand2));
    }
}
