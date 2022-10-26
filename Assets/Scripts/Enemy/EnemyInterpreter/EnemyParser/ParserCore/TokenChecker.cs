#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenStreamChecker
{
    public delegate ParseResult<N> ParserFunction<N>(TokenStreamPointer pointer) where N : notnull;

    private static Dictionary<ScriptToken.Type, string> reservedWordMap = EnemyLexer.mapFromTokenTypeToReservedWord;
    private static string ConvertTypeToString(ScriptToken.Type type)
    {
        if (!reservedWordMap.TryGetValue(type, out string reservedWords))
        {
            throw new Exception($"The type `{type}` is not defined as Token.");
        }
        return reservedWords;
    }
    private static string ConvertToString(ScriptToken token)
    {
        switch (token.type)
        {
            case ScriptToken.Type.INT_LITERAL:
                return token.int_val.ToString();
            case ScriptToken.Type.FLOAT_LITERAL:
                return token.float_val.ToString();
            case ScriptToken.Type.SYMBOL_ID:
                return token.user_defined_symbol.ToString();
        }
        return ConvertTypeToString(token.type);
    }

    private static bool TheSameTokens(ScriptToken tokenA, string tokenBInString)
        => ConvertToString(tokenA).Equals(tokenBInString);

    private static readonly ScriptToken.Type[] allowedTokenTypeList = {
            ScriptToken.Type.SYMBOL_ID,
            ScriptToken.Type.INT_LITERAL,
            ScriptToken.Type.FLOAT_LITERAL
    };
    private TokenStream target;
    public TokenStreamPointer CurrentPointer => target.CurrentPointer;
    private bool isSatisfiedInTheConditionSequence = true;
    private bool shouldBeSatisfied;
    private int initialPointer;
    private ParseException? parseException;

    public bool IsSatisfied => isSatisfiedInTheConditionSequence;

    public TokenStreamChecker(TokenStream target, bool shouldBeSatisfied, int initialPointer)
    {
        this.target = target;
        this.shouldBeSatisfied = shouldBeSatisfied;
        this.initialPointer = initialPointer;
    }

    private void RecognizeFailed(string reason, ScriptToken token)
    {
        isSatisfiedInTheConditionSequence = false;
        parseException = ParseException.Information(reason, token);
        target.RecoverPointer(initialPointer);
        if (shouldBeSatisfied) throw parseException;
    }

    public TokenStreamChecker Expect(string tokenInString)
    {
        var nextToken = target.Read();
        if (!TheSameTokens(nextToken, tokenInString)) RecognizeFailed($"expected `{tokenInString}` but `{ConvertToString(nextToken)}` is coming.", nextToken);
        return this;
    }

    public TokenStreamChecker Maybe(string tokenInString)
    {
        if (TheSameTokens(target.Lookahead(), tokenInString)) target.Read();
        return this;
    }
    public TokenStreamChecker ExpectMulti(string tokenInString)
    {
        while (TheSameTokens(target.Lookahead(), tokenInString)) target.Read();
        return this;
    }

    /// <summary>
    /// 次のトークンが可変語であることを期待します。
    /// </summary>
    /// <param name="captured">次のトークン</param>
    /// <returns></returns>
    public TokenStreamChecker ExpectVariable(out ScriptToken captured)
    {
        var nextToken = target.Read();
        if (!allowedTokenTypeList.Contains(nextToken.type)) RecognizeFailed($"expected something of variable tokens but `{nextToken.type}` is coming.", nextToken);
        captured = nextToken;
        return this;
    }
    public TokenStreamChecker ExpectSymbolID(out string captured)
    {
        var nextToken = target.Read();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID) RecognizeFailed($"expected something of symbolID but `{nextToken.type}` is coming.", nextToken);
        captured = nextToken.user_defined_symbol;
        return this;
    }
    public TokenStreamChecker ExpectConsumedBy<N>(ParserFunction<N> parser, out N captured) where N : notnull
    {
        var parseResult =
            parser
            .Invoke(target.CurrentPointer)
            .ApplyIfSucceeded(ref target)
            .ShouldSucceed();
        captured = parseResult.ParsedNode;

        return this;
    }



    public TokenStreamChecker MaybeSymbolID(out string? captured)
    {
        var nextToken = target.Lookahead();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID)
        {
            captured = null;
            return this;
        }
        captured = nextToken.user_defined_symbol;
        target.Read();
        return this;
    }

    public TokenStreamChecker ExpectMultiSymbolID(out List<string> captured)
    {
        captured = new();
        while (true)
        {
            MaybeSymbolID(out string? result);
            if (result == null) break;
            captured.Add(result ?? throw new Exception());
        }
        return this;
    }
    public TokenStreamChecker MaybeVariable(out ScriptToken? captured)
    {
        var nextToken = target.Lookahead();
        if (!allowedTokenTypeList.Contains(nextToken.type))
        {
            captured = null;
            return this;
        }
        captured = nextToken;
        target.Read();
        return this;
    }
    public TokenStreamChecker ExpectMultiVariable(out List<ScriptToken> captured)
    {
        captured = new();
        while (true)
        {
            MaybeVariable(out ScriptToken? result);
            if (result == null) break;
            captured.Add(result ?? throw new Exception());
        }
        return this;
    }
    public TokenStreamChecker MaybeConsumedBy<N>(ParserFunction<N> parser, out N? captured) where N : notnull
    {
        var parseResult =
            parser
            .Invoke(target.CurrentPointer)
            .ApplyIfSucceeded(ref target);
        captured = parseResult.ParsedNodeNullable;
        return this;
    }
    public TokenStreamChecker ExpectMultiComsumer<N>(ParserFunction<N> parser, out List<N> captured) where N : notnull
    {
        captured = new();
        while (true)
        {
            try
            {
                MaybeConsumedBy(parser, out N? result);
                if (result == null) break;
                captured.Add(result ?? throw new Exception());
            }
            catch (ParseException e)  // parser 内で should を使っていると exception が返ってくるので, 失敗扱いで処理をする.
            {
                break;
            }
        }
        return this;
    }
    public TokenStreamChecker ExpectMultiComsumerAtLeast1<N>(ParserFunction<N> parser, out List<N> captured) where N : notnull
    {
        ExpectConsumedBy(parser, out N firstOne);
        ExpectMultiComsumer(parser, out List<N> successor);
        successor.Prepend(firstOne);
        captured = successor;
        return this;
    }
}
