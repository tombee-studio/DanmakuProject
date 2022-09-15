#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
public class TokenStreamChecker
{
    public delegate ParseResult<N> ParserFunction<N>(TokenStreamPointer pointer) where N:notnull;

    private static Dictionary<ScriptToken.Type, string> reservedWordMap = EnemyLexer.mapFromTokenTypeToReservedWord;
    private static string ConvertToString(ScriptToken.Type type)
    {
        if (reservedWordMap.TryGetValue(type, out string reservedWords)) throw new Exception($"The type `{type}` is not defined as Token.");
        return reservedWords;
    }

    private static bool TheSameTokens(ScriptToken tokenA, string tokenBInString)
        => ConvertToString(tokenA.type).Equals(tokenBInString);

    private static readonly ScriptToken.Type[] allowedTokenTypeList = {
            ScriptToken.Type.SYMBOL_ID,
            ScriptToken.Type.INT_LITERAL,
            ScriptToken.Type.FLOAT_LITERAL
    };
    private TokenStream target;
    private bool isSatisfiedInTheConditionSequence = true;
    private bool shouldBeSatisfied;
    private ParseException? parseException;

    public TokenStreamChecker(TokenStream target, bool shouldBeSatisfied)
    {
        this.target = target;
        this.shouldBeSatisfied = shouldBeSatisfied;
    }

    private void RecognizeFailed(string reason)
    {
        isSatisfiedInTheConditionSequence = false;
        parseException = ParseException.Information(reason, target.CurrentPointer);
        if (shouldBeSatisfied) throw parseException;
    }

    public TokenStreamChecker Expect(string tokenInString)
    {
        var nextToken = target.Read();
        if (!TheSameTokens(nextToken, tokenInString)) RecognizeFailed($"expected `{tokenInString}` but `{ConvertToString(nextToken.type)}` is coming.");
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

    public TokenStreamChecker ExpectVariable(out ScriptToken captured)
    {
        var nextToken = target.Read();
        if (!allowedTokenTypeList.Contains(nextToken.type)) RecognizeFailed($"expected something of variable tokens but `{nextToken.type}` is coming.");
        captured = nextToken;
        return this;
    }
    public TokenStreamChecker ExpectSymbolID(out string captured)
    {
        var nextToken = target.Read();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID) RecognizeFailed($"expected something of symbolID but `{nextToken.type}` is coming.");
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
        while (true) {
            MaybeConsumedBy(parser, out N? result);
            if (result == null) break;
            captured.Add(result ?? throw new Exception());
        }
        return this;
    }
}
