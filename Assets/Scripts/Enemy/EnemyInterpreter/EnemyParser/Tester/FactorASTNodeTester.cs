using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

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

    public void test_FactorASTNode_compile1()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "MUL 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.MULTIPLY;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new FactorASTNode(arithmeticOperator2,
            null, new NumberASTNode(1));
        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            left, right);
        foreach (var instructions in factor.Compile(new Dictionary<string, int>())
            .Zip(testCodes, (first, second) => (first, second))) {
            var (i1, i2) = instructions;
            Assert.AreEqual($"{i1}", i2);
        }
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

    public void test_FactorASTNode_compile2()
    {
        string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "DIV 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.DIVIDE;

        var arithmeticOperator2 = new ScriptToken();
        arithmeticOperator2.type = ScriptToken.Type.NONE;
        var left = new FactorASTNode(arithmeticOperator2,
            null, new NumberASTNode(1));
        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            left, right);
        foreach (var instructions in factor.Compile(new Dictionary<string, int>())
            .Zip(testCodes, (first, second) => (first, second)))
        {
            var (i1, i2) = instructions;
            Assert.AreEqual($"{i1}", i2);
        }
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

    public void test_FactorASTNode_compile3()
    {
        /**
         * TODO: MOD演算が実装されたら実装し直してください
         */
        try
        {
            string[] testCodes = {
            "PUSH 1",
            "PUSH 1",
            "MOD 2"
        };
            var arithmeticOperator1 = new ScriptToken();
            arithmeticOperator1.type = ScriptToken.Type.MOD;

            var arithmeticOperator2 = new ScriptToken();
            arithmeticOperator2.type = ScriptToken.Type.NONE;
            var left = new FactorASTNode(arithmeticOperator2,
                null, new NumberASTNode(1));
            var right = new NumberASTNode(1);
            var factor = new FactorASTNode(arithmeticOperator1,
                left, right);
            foreach (var instructions in factor.Compile(new Dictionary<string, int>())
                .Zip(testCodes, (first, second) => (first, second)))
            {
                var (i1, i2) = instructions;
                Assert.AreEqual($"{i1}", i2);
            }
        }
        catch (NotImplementedException) {
            return;
        }
        Assert.IsTrue(false);
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

    public void test_FactorASTNode_compile4()
    {
        string[] testCodes = {
            "PUSH -1",
            "PUSH 1",
            "MUL 2"
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.SUB;

        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            null, right);

        foreach (var instructions in factor.Compile(new Dictionary<string, int>())
            .Zip(testCodes, (first, second) => (first, second)))
        {
            var (i1, i2) = instructions;
            Assert.AreEqual($"{i1}", i2);
        }
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

    public void test_FactorASTNode_compile5()
    {
        string[] testCodes = {
            "PUSH 1",
        };
        var arithmeticOperator1 = new ScriptToken();
        arithmeticOperator1.type = ScriptToken.Type.NONE;

        var right = new NumberASTNode(1);
        var factor = new FactorASTNode(arithmeticOperator1,
            null, right);

        foreach (var instructions in factor.Compile(new Dictionary<string, int>())
            .Zip(testCodes, (first, second) => (first, second)))
        {
            var (i1, i2) = instructions;
            Assert.AreEqual($"{i1}", i2);
        }
    }
}

