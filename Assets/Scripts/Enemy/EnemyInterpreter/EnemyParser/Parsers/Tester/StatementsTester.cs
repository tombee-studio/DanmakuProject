using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public partial class EnemyParserTester
{
    void test_break_st1()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "break",
            ScriptToken.Type.BREAK));
        var parser = new EnemyParser();
        var node = parser.ParseBreakSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"break\n");
    }
    void test_block(){
        //TODO:
    }

    void test_decl_st1()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "int",
            ScriptToken.Type.INT));
        tokens.Add(ScriptToken.GenerateToken(
            "a",
            ScriptToken.Type.SYMBOL_ID));
        var parser = new EnemyParser();
        var node = parser.ParseDeclarationSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"int a\n");
    }

    void test_decl_st2()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "float",
            ScriptToken.Type.FLOAT));
        tokens.Add(ScriptToken.GenerateToken(
            "a",
            ScriptToken.Type.SYMBOL_ID));
        var parser = new EnemyParser();
        var node = parser.ParseDeclarationSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"float a\n");
    }
    void test_expst1()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        var parser = new EnemyParser();
        var node = parser.ParseExpSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"1\n");
    }

    void test_expst2()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        tokens.Add(ScriptToken.GenerateToken(
            "+",
            ScriptToken.Type.PLUS));
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        var parser = new EnemyParser();
        var node = parser.ParseExpSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"1+1\n");
    }
    void test_ifElse(){
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.IF))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_LEFT))
            .Append(ScriptToken.GenerateToken("0", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_RIGHT))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.ELSE))
            .Append(ScriptToken.GenerateToken("91", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseIfSt,
            "if(0)\n\t42\nelse\n\t91\n"
        );
    }
    void test_if(){
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.IF))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_LEFT))
            .Append(ScriptToken.GenerateToken("1", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_RIGHT))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseIfSt,
            "if(1)\n\t42\n"
        );
    }
    void test_assign_st1()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "a",
            ScriptToken.Type.SYMBOL_ID));
        tokens.Add(ScriptToken.GenerateToken(
            "=",
            ScriptToken.Type.ASSIGNMENT));
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        var parser = new EnemyParser();
        var node = parser.ParseAssignSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"a = 1\n");
    }

    void test_assign_st2()
    {
        var tokens = new List<ScriptToken>();
        tokens.Add(ScriptToken.GenerateToken(
            "a",
            ScriptToken.Type.SYMBOL_ID));
        tokens.Add(ScriptToken.GenerateToken(
            "=",
            ScriptToken.Type.ASSIGNMENT));
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        tokens.Add(ScriptToken.GenerateToken(
            "+",
            ScriptToken.Type.PLUS));
        tokens.Add(ScriptToken.GenerateToken(
            "1",
            ScriptToken.Type.INT_LITERAL));
        var parser = new EnemyParser();
        var node = parser.ParseAssignSt(new TokenStreamPointer(tokens));
        Assert.AreEqual(node.ParsedNode.Print(0), $"a = 1+1\n");
    }

    void test_repeat()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.REPEAT))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_LEFT))
            .Append(ScriptToken.GenerateToken("32", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.BRACKET_RIGHT))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseRepeatSt,
            "repeat(32)42\n"
        );
    }
}
