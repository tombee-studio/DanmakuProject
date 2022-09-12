using System;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_ExpASTNode1()
    {
        var exp = new ExpASTNode(
            new EqualityExpASTNode(
                new RelationalExpASTNode(
                    new FactorExpASTNode(
                        new UnaryExpASTNode(ScriptToken.GenerateToken("", ScriptToken.Type.SUB),
                            new PrimaryExpASTNode(7)
                        ),
                        ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY),
                        new PrimaryExpASTNode(
                            new TermExpASTNode(
                                new PrimaryExpASTNode(2),
                                ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                                new PrimaryExpASTNode(3)
                            )
                        )
                    ),
                    ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN),
                    new FactorExpASTNode(
                        new PrimaryExpASTNode(7),
                        ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY),
                        new PrimaryExpASTNode(13)
                    )
                ),
                ScriptToken.GenerateToken("", ScriptToken.Type.EQUAL),
                new PrimaryExpASTNode(1)
            )
        );
        string[] testCodes = {
            "PUSH 7",
            "PUSH -1",
            "MUL 0",
            "PUSH 2",
            "PUSH 3",
            "ADD 0",
            "MUL 0",
            "PUSH 7",
            "PUSH 13",
            "MUL 0",
            "GT 0",
            "PUSH 1",
            "EQ 0",
        };
        checkGeneratedInstructionIsSame(testCodes, exp);
        checkVMReturnValueFromSubProgram(exp, (((-7 * (2 + 3) > 7 * 13) ? 1 : 0) == 1) ? 1 : 0);
        Assert.AreEqual(exp.Print(0), "-7*(2+3)>7*13==1");
    }
}

