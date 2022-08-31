using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyLexerTester : Tester
{
    EnemyLexer enemyLexer = new();

    void assertIsSequenceEqual(string code, string lexedCode)
    {
        var resultList = enemyLexer.Lex(code);
        var expectedList = parseTokenSequence(lexedCode);
        var zippedList = resultList
            .Zip(expectedList, (result, expected) =>
                new {
                    result = result,
                    expected = expected
                });
        foreach (var element in zippedList) assertIsEqual(element.result, element.expected);

    }
    void assertIsEqual(ScriptToken expected, ScriptToken result)
    {
        if (expected.type == result.type)
            throw new Exception($"Token Type mismatched: expected {expected.type} -- result {result.type}");
        if (
            expected.user_defined_symbol == result.user_defined_symbol &&
            expected.int_val == result.int_val &&
            expected.float_val == result.float_val
        ) throw new Exception($"Token value mismatched: expected {expected.user_defined_symbol}, {expected.int_val}, {expected.float_val}  -- result {result.user_defined_symbol}, {result.int_val}, {expected.float_val}");
        return;
    }
    /**
     * 文字列で記述されたトークン列を解釈し、それに該当するTokenの列を生成する。
     * 例:改行を挟んで記述する。 
     * > parseLexerSequence(@"
     *      ID abd23
     *      (
     *      Int 3
     *      +
     *      Float 2.3
     *      )
     * ")
     * 
     * []
     *  
     */
    List<ScriptToken> parseTokenSequence(string lexerCode)
    {
        var parsedTokenSequence = new List<ScriptToken>();
        foreach (var line in lexerCode.Split("\n"))
        {
            var tokenTypeStr = line.Trim();
            if (tokenTypeStr.Length == 0) continue;
            parsedTokenSequence.Add(
                GetReservedTokenTypeFromValue(tokenTypeStr)
                ?? GetVariableTokenTypeFromValue(tokenTypeStr)
            );


        }
        return parsedTokenSequence;

    }
    ScriptToken? GetReservedTokenTypeFromValue(string tokenString)
    {
        foreach (var key in EnemyLexer.mapFromTokenTypeToReservedWord.Keys)
        {
            string value;
            if (!EnemyLexer.mapFromTokenTypeToReservedWord.TryGetValue(key, out value)) continue;
            if (value != tokenString) continue;
            return ScriptToken.GenerateToken(tokenString, key);
        }
        return null;

    }
    ScriptToken GetVariableTokenTypeFromValue(string tokenLine)
    {
        if (!tokenLine.Contains(" ")) throw new Exception($"The count of tokens is fewer than two in this line.: {tokenLine}.");
        var devidedLine = tokenLine.Split(" ");
        var label = devidedLine[0];
        var variableWord = devidedLine[1];
        var tokenTypeInEnum = label switch
        {
            "symbolID" => ScriptToken.Type.USER_DEFINED_SYMBOL,
            "int" => ScriptToken.Type.INT_LITERAL,
            "float" => ScriptToken.Type.FLOAT_LITERAL,
            _ => throw new Exception($"Invalid Token Label {label}.")
        };
        return ScriptToken.GenerateToken(variableWord, tokenTypeInEnum);

    }
}
