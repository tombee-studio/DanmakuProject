using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

// TODO: EnemyASTNodeTester ではなく, EnemyParserTester ではないか?
public partial class EnemyParserTester
{
    public void ValidatePrintResult<N>(
        List<ScriptToken> tokens,
        TokenStreamChecker.ParserFunction<N> Parse,
        string expected
    ) where N : ASTNode
    {
        var pointer = new TokenStreamPointer(tokens);
        var result = Parse(pointer);
        Assert.AreEqual(expected, result.ParsedNode.Print(0));
    }
    public void test_ParseMod()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("133", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.MOD))
            .Append(ScriptToken.GenerateToken("91", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseFactorExpASTNode,
            "133%91"
        );
    }
    public void test_ParseDiv()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("882", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.DIVIDE))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseFactorExpASTNode,
            "882/42"
        );
    }
    public void test_ParseMlt()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.SUB))
            .Append(ScriptToken.GenerateToken("7", ScriptToken.Type.INT_LITERAL))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY))
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.SUB))
            .Append(ScriptToken.GenerateToken("6", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseFactorExpASTNode,
            "-7*-6"
        );
    }
    public void test_ParseFactor()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.SUB))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseFactorExpASTNode,
            "-42"
        );
    }
    public void test_ParseMinus()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("", ScriptToken.Type.SUB))
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParseUnaryExpASTNode,
            "-42"
        );
    }
    public void test_ParseUnary()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens, 
            new EnemyParser().ParseUnaryExpASTNode,
            "42"
        );
    }
    public void test_ParseIntLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParsePrimaryExpASTNode,
            "42"
        );
    }
    public void test_ParseFloatLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("3.14", ScriptToken.Type.FLOAT_LITERAL))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParsePrimaryExpASTNode,
            "3.14"
        );
    }
    public void test_ParseSymbolIDLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("foo", ScriptToken.Type.SYMBOL_ID))
            .ToList();
        ValidatePrintResult(
            tokens,
            new EnemyParser().ParsePrimaryExpASTNode,
            "foo"
        );
    }
}
