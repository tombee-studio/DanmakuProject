using System;
using System.Collections.Generic;
using MatchingFunction = System.Func<string, EnemyLexer.MatchResult>;

public class EnemyLexerTester : Tester
{
    EnemyLexer enemyLexer = new();
    void test_simpleCode()
    {
        
    }
    /**
     * 文字列で記述されたトークン列を解釈し、それに該当するTokenの列を生成する。
     * 例:改行を挟んで記述する。 
     * > parseLexerSequence("""
     *      (
     *      3
     *      +
     *      2
     *      )
     * """)
     * 
     * []
     *  
     */
    List<ScriptToken.Type> parseTokenSequence(string lexerCode)
    {
        var parsedTokenSequence = new List<ScriptToken.Type>();
        foreach (var line in lexerCode.Split("\n"))
        {
            var sentence = line.Trim();
            if (!sentence.Contains(" ")){
                var tokenType = Enum.Parse<ScriptToken.Type>(sentence);
                parsedTokenSequence.Add(tokenType);
            } else
            {
                
            }
        }
        return parsedTokenSequence;
        
    }
    ScriptToken.Type GetReservedTokenTypeFromValue(string tokenTypeStr)
    {
        foreach (var key in EnemyLexer.mapFromTokenTypeToReservedWord.Keys)
        {
            string value;
            if (!EnemyLexer.mapFromTokenTypeToReservedWord.TryGetValue(key, out value)) continue;
            if (value != tokenTypeStr) continue;
            return key;
        }
        throw new Exception("該当する予約語が存在しませんでした。");
    }
    ScriptToken.Type GetVariableTokenTypeFromValue(string tokenTypeStr)
    {
        foreach (var key in EnemyLexer.mapFromTokenTypeToReservedWord.Keys)
        {
            MatchingFunction value;
            if (!EnemyLexer.mapFromTokenTypeToVariableWord.TryGetValue(key, out value)) continue;
            if (value.Invoke(tokenTypeStr)) continue;
            return key;
        }
        throw new Exception("該当する予約語が存在しませんでした。");
    }
}
