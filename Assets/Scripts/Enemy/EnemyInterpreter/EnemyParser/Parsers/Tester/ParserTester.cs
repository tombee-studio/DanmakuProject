using System;
public partial class ParserTester : Tester
{
    protected override Tester cloneThisObject()
    {
        return new ParserTester();
    }
}
