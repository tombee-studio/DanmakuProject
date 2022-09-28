using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

// TODO: EnemyASTNodeTester ではなく, EnemyParserTester ではないか?
public partial class EnemyParserTester
{
    public void ValidatePrintResult(List<ScriptToken> tokens, string expected){
        var parser = new EnemyParser();
        var pointer = new TokenStreamPointer(tokens);
        var result = parser.ParsePrimaryExpASTNode(pointer);
        Assert.AreEqual(expected, result.ParsedNode.Print(0));
    }
    public void test_ParseIntLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("42", ScriptToken.Type.INT_LITERAL))
            .ToList();
        ValidatePrintResult(tokens, "42");
    }
    public void test_ParseFloatLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("3.14", ScriptToken.Type.FLOAT_LITERAL))
            .ToList();
        ValidatePrintResult(tokens, "3.14");
    }
    public void test_ParseSymbolIDLiteral()
    {
        var tokens = new List<ScriptToken>()
            .Append(ScriptToken.GenerateToken("foo", ScriptToken.Type.SYMBOL_ID))
            .ToList();
        ValidatePrintResult(tokens, "foo");
    }
}
