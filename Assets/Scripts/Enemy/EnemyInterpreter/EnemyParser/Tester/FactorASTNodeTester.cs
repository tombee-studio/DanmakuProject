using System;
using UnityEngine;
using UnityEngine.Assertions;

public partial class EnemyASTNodeTester
{
    public void test_FactorASTNode_print1() {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MULTIPLY;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new FactorASTNode(arithmeticOperator2,
            null, new NumberASTNode(1));
        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            left, right);

        Assert.AreEqual(factor.Print(0), "1*1");
    }

    public void test_FactorASTNode_print2()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new FactorASTNode(arithmeticOperator2,
            null, new NumberASTNode(1));
        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            left, right);

        Assert.AreEqual(factor.Print(0), "1/1");
    }

    public void test_FactorASTNode_print3()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MOD;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new FactorASTNode(arithmeticOperator2,
            null, new NumberASTNode(1));
        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            left, right);

        Assert.AreEqual(factor.Print(0), "1%1");
    }

    public void test_FactorASTNode_print4()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;

        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            null, right);

        Assert.AreEqual(factor.Print(0), "-1");
    }

    public void test_FactorASTNode5()
    {
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.NONE;

        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            null, right);

        Assert.AreEqual(factor.Print(0), "1");
    }
}

