using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public partial class EnemyParserTester
{
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
}
