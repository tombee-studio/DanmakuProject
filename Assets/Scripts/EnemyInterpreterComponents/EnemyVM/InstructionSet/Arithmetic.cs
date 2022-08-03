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
        // スタックマシン{push ope1, push ope2, MUL} => push ope1 - ope2
        // スタックからPopされる、オペランドの順番はope2, ope1である。
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
