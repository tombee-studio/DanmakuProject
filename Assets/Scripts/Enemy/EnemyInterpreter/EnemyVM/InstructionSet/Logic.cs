using System;
public partial class EnemyVM
{

    private void And()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 != 0 && operand2 != 0 ? 1 : 0);
    }

    private void Or()
    {
        PrimitiveValue operand2 = PopFromStack();
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 != 0 || operand2 != 0 ? 1 : 0);
    }

    private void Not()
    {
        PrimitiveValue operand1 = PopFromStack();
        PushIntoStack(operand1 == 0 ? 1 : 0);
    }
}
