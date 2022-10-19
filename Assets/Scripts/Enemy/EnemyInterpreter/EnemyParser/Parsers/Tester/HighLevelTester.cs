using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public partial class EnemyParserTester: Tester
{
    void test_behaviour()
    {
        var script = @"
        behavior enemy001 {
        bullet >>
            ID: 0
                alert(23, 23)
                alert(23)
        action >>
            int x
        }";
        var tokens = (new EnemyLexer()).Lex(script);
        Assert.AreEqual(tokens.Count(), 23);
        var result = (new EnemyParser()).
            ParseBehaviour(new TokenStreamPointer(tokens));
        Debug.Log(result.ParsedNode.Print(0));
    }
}
