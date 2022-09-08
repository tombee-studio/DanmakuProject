using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Linq;
using UnityEngine;

public partial class EnemyLexerTester : Tester
{
    
    EnemyLexer enemyLexer = new();
    protected override Tester cloneThisObject()
    {
        return new EnemyLexerTester();
    }
    public void assertIsSequenceEqual(string code, string lexedCode)
    {
        var resultList = enemyLexer.Lex(code);
        var expectedList = parseTokenSequence(lexedCode);
        var zippedList = resultList
            .Zip(expectedList, (result, expected) =>
                new {
                    result = result,
                    expected = expected
                });
        foreach (var element in zippedList)
        {
            assertIsEqual(element.expected, element.result);
        }

    }
    public void assertSequenceThrow<T>(string code, string lexedCode)
        where T:Exception
    {
        try
        {
            assertIsSequenceEqual(code, lexedCode);
            throw new NoException();
        }
        catch (T)
        {
        }
        catch (NoException e)
        {
            throw new AssertionException(e.Message, "");
        }
        catch (Exception error)
        {
            throw new AssertionException("The expected error wasn't thrown", error.Message);
        }

    }
    void assertIsEqual(ScriptToken expected, ScriptToken result)
    {
        if (expected.type != result.type)
            throw new TokenTypeMismatchedException($"expected {expected.type} -- result {result.type}");
        if (
            expected.user_defined_symbol != result.user_defined_symbol ||
            expected.int_val != result.int_val ||
            expected.float_val != result.float_val
        ) throw new TokenValueMismatchedException(result, expected);
        return;
    }
    /**
     * 文字列で記述されたトークン列を解釈し、それに該当するTokenの列を生成する。
     * 例:改行を挟んで記述する。 
     * > parseLexerSequence(@"
     *      symbolID abd23
     *      (
     *      int 3
     *      +
     *      float 2.3
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
        if (!tokenLine.Contains(" ")) throw new InvalidTokenSequenceException($"The count of tokens is fewer than two in this line.: {tokenLine}.");
        var devidedLine = tokenLine.Split(" ");
        var label = devidedLine[0];
        var variableWord = devidedLine[1];
        var tokenTypeInEnum = label switch
        {
            //TODO: symbolIDをsymbolに変えたい
            "symbolID" => ScriptToken.Type.SYMBOL_ID,
            "int" => ScriptToken.Type.INT_LITERAL,
            "float" => ScriptToken.Type.FLOAT_LITERAL,
            _ => throw new InvalidTokenSequenceException($"Invalid Token Label {label}.")
        };
        return ScriptToken.GenerateToken(variableWord, tokenTypeInEnum);

    }
}
