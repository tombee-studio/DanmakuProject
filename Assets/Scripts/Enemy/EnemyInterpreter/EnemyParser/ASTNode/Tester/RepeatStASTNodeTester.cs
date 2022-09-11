using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    // TODO: 実行テストは block のテストに書いておく
    void test_RepeatStASTNode()
    {
        int counterAddr = 0;
        string[] testCodes = {
            // i = 0
            $"PUSH {counterAddr}",
            "PUSH 0",
            "STORE 0",
            // :loop_begin
            // if (not(i<N)) goto :end_loop
            $"PUSH {counterAddr}",
            "LOAD 0",
            "PUSH 100",
            "LT 0",
            "JNE 10",
            // 3+5
            "PUSH 3",
            "PUSH 5",
            "ADD 2",
            // i++
            $"PUSH {counterAddr}",
            "LOAD 0",
            "PUSH 1",
            "ADD 2",
            $"PUSH {counterAddr}",
            "STORE 0",
            // goto :loop_begin
            "JMP -15"
            // :end_loop
        };
        var node = new RepeatStASTNode(100,
            new TermExpASTNode(
                new PrimaryExpASTNode(3),
                ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                new PrimaryExpASTNode(5)
            )
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual("repeat(100)3+5\n", node.Print(0));
    }
}

