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
    private static string CompareToken(ScriptToken tokenA, string tokenBInString)
        => ConvertToString(tokenA).Equals(tokenInString);
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
        if (!) RecognizeFailed($"expected `{tokenInString}` but `{ConvertToString(nextToken.type)}` is coming.");
        return this;
    }

    public TokenStreamChecker Maybe(string tokenInString)
    {
        var nextToken = target.Lookahead();
        if (ConvertToString(nextToken.type).Equals(tokenInString)) target.Read();
        return this;
    }
    //TODO 0個以上連続するトークンに対するアサーションを作成する。
    // ExpectMulti, MaybeMulti
    // ExpectMultiVariable, ExpectMultiSymbolID, ExpectMultiConsumedBy
    // MaybeMultiVariable, MaybeMultiSymbolID, MaybeMultiConsumedBy
    public TokenStreamChecker ExpectMulti(string tokenInString)
    {
        while (ConvertToString(target.Lookahead()))
        
        
    }

    public TokenStreamChecker MaybeMulti(string tokenInString)
    {
        var nextToken = target.Lookahead();
        if (ConvertToString(nextToken.type).Equals(tokenInString)) target.Read();
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
    public TokenStreamChecker MaybeConsumedBy<N>(ParserFunction<N> parser, out N? captured) where N : notnull
    {
        var parseResult =
            parser
            .Invoke(target.CurrentPointer)
            .ApplyIfSucceeded(ref target);
        captured = parseResult.ParsedNodeNullable;
        return this;
    }
}
