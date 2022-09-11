using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
public partial class EnemyASTNodeTester
{
    void test_DecralationBlockStASTNodeTester()
    {
        // TODO: 変数のメモリと定数のメモリが共有なのでプログラムの開始時に変数の分だけプッシュ命令を入れる
        string[] testCodes = {
            "PUSH 42",
            "PUSH 0",
            "STORE 0",
            "PUSH 91",
            "PUSH 1",
            "STORE 0",
            "PUSH 1",
            "LOAD 0",
        };
        var node = new BlockStASTNode(
            new List<StatementASTNode>()
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "i"))
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "j"))
            .Append(new AssignStASTNode("i", new PrimaryExpASTNode(42)))
            .Append(new AssignStASTNode("j", new PrimaryExpASTNode(91)))
            .Append(new PrimaryExpASTNode("j"))
            .ToList()
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, 91);
        Assert.AreEqual(
            "\n{\n" +
            "\t" + "int i\n" +
            "\t" + "int j\n" +
            "\t" + "i = 42\n" +
            "\t" + "j = 91\n" +
            "\t" + "j\n" +
            "}\n",
            node.Print(0)
        );
    }
    void test_AssignmentBlockStASTNodeTester()
    {
        string[] testCodes = {
            "PUSH 0",
            "PUSH 0",
            "STORE 0",
            "PUSH 42",
            "PUSH 1",
            "STORE 0",
            "PUSH 0",
            "LOAD 0",
            "PUSH 1",
            "LOAD 0",
            "SUB 0",
        };
        var node = new BlockStASTNode(
            new List<StatementASTNode>()
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "i"))
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "j"))
            .Append(new AssignStASTNode("i", new PrimaryExpASTNode(0)))
            .Append(new AssignStASTNode("j", new PrimaryExpASTNode(42)))
            .Append(new TermExpASTNode(
                new PrimaryExpASTNode("i"),
                ScriptToken.GenerateToken("", ScriptToken.Type.SUB),
                new PrimaryExpASTNode("j")
            ))
            .ToList()
        );
        checkGeneratedInstructionIsSame(testCodes, node);
        Assert.AreEqual(
            "\n{\n" +
            "\t" + "int i\n" +
            "\t" + "int j\n" +
            "\t" + "i = 0\n" +
            "\t" + "j = 42\n" +
            "\t" + "i-j\n" +
            "}\n",
            node.Print(0)
        );
        checkVMReturnValueFromSubProgram(node, -42);
    }

    void test_RepeatBlockStASTNodeTester()
    {
        // 1 から 10 までの総和を求める
        string[] scriptCodes = {
            "",
            "{",
            "\t" + "int i",
            "\t" + "int sum",
            "\t" + "i = 1",
            "\t" + "sum = 0",
            "\t" + "repeat(10)",
            "\t" + "{",
            "\t" + "\t" + "sum = sum+i",
            "\t" + "\t" + "i = i+1",
            "\t" + "}",
            "\t" + "sum",
            "}",
            ""
        };
        string[] testCodes = {
            // i = 1
            "PUSH 1",
            "PUSH 0",
            "STORE 0",
            // sum = 0
            "PUSH 0",
            "PUSH 1",
            "STORE 0",
            // counter = 0
            "PUSH 0",
            "PUSH 2",
            "STORE 0",
            // :loop
            // if (not(counter < 10)) goto :end_loop
            "PUSH 2",
            "LOAD 0",
            "PUSH 10",
            "LT 0",
            "JNE 20",
            // sum = sum + i
            "PUSH 1",
            "LOAD 0",
            "PUSH 0",
            "LOAD 0",
            "ADD 0",
            "PUSH 1",
            "STORE 0",
            // i = i + 1
            "PUSH 0",
            "LOAD 0",
            "PUSH 1",
            "ADD 0",
            "PUSH 0",
            "STORE 0",
            // counter++
            "PUSH 2",
            "LOAD 0",
            "PUSH 1",
            "ADD 0",
            "PUSH 2",
            "STORE 0",
            // goto :loop
            "JMP -25",
            // :end_loop
            "PUSH 1",
            "LOAD 0",
        };
        var node = new BlockStASTNode(
            new List<StatementASTNode>()
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "i"))
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "sum"))
            .Append(new AssignStASTNode("i", new PrimaryExpASTNode(1)))
            .Append(new AssignStASTNode("sum", new PrimaryExpASTNode(0)))
            .Append(new RepeatStASTNode(10, new BlockStASTNode(
                new List<StatementASTNode>()
                .Append(new AssignStASTNode("sum",
                    new TermExpASTNode(
                        new PrimaryExpASTNode("sum"),
                        ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                        new PrimaryExpASTNode("i")
                    )
                ))
                .Append(new AssignStASTNode("i",
                    new TermExpASTNode(
                        new PrimaryExpASTNode("i"),
                        ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                        new PrimaryExpASTNode(1)
                    )
                ))
                .ToList()
            )))
            .Append(new PrimaryExpASTNode("sum"))
            .ToList()
        );
        checkIsPrintScript(scriptCodes, node);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, 55);
    }

    void test_BreakBlockStASTNodeTester()
    {
        // 1 から 10 までの総和を求める
        string[] scriptCodes = {
            "",
            "{",
            "\t" + "int i",
            "\t" + "int sum",
            "\t" + "i = 1",
            "\t" + "sum = 0",
            "\t" + "repeat(10)",
            "\t" + "{",
            "\t" + "\t" + "if(i>6)",
            "\t" + "\t" + "\t" + "break",
            "\t" + "\t" + "sum = sum+i",
            "\t" + "\t" + "i = i+1",
            "\t" + "}",
            "\t" + "sum",
            "}",
            ""
        };
        string[] testCodes = {
            // i = 1
            "PUSH 1",
            "PUSH 0",
            "STORE 0",
            // sum = 0
            "PUSH 0",
            "PUSH 1",
            "STORE 0",
            // counter = 0
            "PUSH 0",
            "PUSH 2",
            "STORE 0",
            // :loop
            // if (not(counter < 10)) goto :end_loop
            "PUSH 2",
            "LOAD 0",
            "PUSH 10",
            "LT 0",
            "JNE 26",
            // if(i<6) {
            "PUSH 0",
            "LOAD 0",
            "PUSH 6",
            "GT 0",
            "JNE 1",
            // break;
            "JMP 20",
            // }
            // sum = sum + i
            "PUSH 1",
            "LOAD 0",
            "PUSH 0",
            "LOAD 0",
            "ADD 0",
            "PUSH 1",
            "STORE 0",
            // i = i + 1
            "PUSH 0",
            "LOAD 0",
            "PUSH 1",
            "ADD 0",
            "PUSH 0",
            "STORE 0",
            // counter++
            "PUSH 2",
            "LOAD 0",
            "PUSH 1",
            "ADD 0",
            "PUSH 2",
            "STORE 0",
            // goto :loop
            "JMP -31",
            // :end_loop
            "PUSH 1",
            "LOAD 0",
        };
        var node = new BlockStASTNode(
            new List<StatementASTNode>()
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "i"))
            .Append(new DeclarationStASTNode(PrimitiveValue.Type.INT, "sum"))
            .Append(new AssignStASTNode("i", new PrimaryExpASTNode(1)))
            .Append(new AssignStASTNode("sum", new PrimaryExpASTNode(0)))
            .Append(new RepeatStASTNode(10, new BlockStASTNode(
                new List<StatementASTNode>()
                .Append(new IfStASTNode(
                    new RelationalExpASTNode(
                        new PrimaryExpASTNode("i"),
                        ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN),
                        new PrimaryExpASTNode(6)
                    ),
                    new BreakStASTNode()
                ))
                .Append(new AssignStASTNode("sum",
                    new TermExpASTNode(
                        new PrimaryExpASTNode("sum"),
                        ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                        new PrimaryExpASTNode("i")
                    )
                ))
                .Append(new AssignStASTNode("i",
                    new TermExpASTNode(
                        new PrimaryExpASTNode("i"),
                        ScriptToken.GenerateToken("", ScriptToken.Type.PLUS),
                        new PrimaryExpASTNode(1)
                    )
                ))
                .ToList()
            )))
            .Append(new PrimaryExpASTNode("sum"))
            .ToList()
        );
        checkIsPrintScript(scriptCodes, node);
        checkGeneratedInstructionIsSame(testCodes, node);
        checkVMReturnValueFromSubProgram(node, 21);
    }

}

