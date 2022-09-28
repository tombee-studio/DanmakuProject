#nullable enable
using System;
using System.Collections.Generic;
public partial class EnemyParser
{
    /**
     * <ID> ((EXP,)* EXP?)の部分。
     * ECP
     * 
     */
    public ParseResult<CallFuncStASTNodeBase> ParseCallFuncStASTNode(TokenStreamPointer pointer)
    {
        if (!TestCallFuncStASTNode(pointer))
            return ParseResult<CallFuncStASTNodeBase>.Failed("This token's line is not function.", "CallFuncStASTNode", pointer);
        var stream = pointer.StartStream();
        stream.should
            .ExpectSymbolID(out string functionID)
            .Expect("(")
            .ExpectMultiComsumer(partialParseOneArg, out List<ExpASTNodeBase> expASTNodes)
            .MaybeConsumedBy(ParseExpASTNode, out var expASTNodeNullable);
        if (expASTNodeNullable != null) expASTNodes.Add(expASTNodeNullable);

        return new(
                new CallFuncStASTNode(functionID, expASTNodes),
                stream.CurrentPointer
        );
    }

    /**
     * (EXP,) の部分
     */
    private ParseResult<ExpASTNodeBase> partialParseOneArg(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (
            !stream.maybe
                .ExpectConsumedBy(ParseExpASTNode, out var exp)
                .Expect(",")
                .IsSatisfied
        ) { return ParseResult<ExpASTNodeBase>.Failed("Arg sequence is finished.", "partialParseOneArg", pointer); }
        return new(
                exp,
                stream.CurrentPointer
        );
    }

    public ParseResult<ExpASTNodeBase> ParseExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    public ParseResult<EqualityExpASTNodeBase> ParseEqualityExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    public ParseResult<RelationalExpASTNodeBase> ParseRelationalExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    public ParseResult<TermExpASTNodeBase> ParseTermExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    public ParseResult<FactorExpASTNodeBase> ParseFactorExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    public ParseResult<UnaryExpASTNodeBase> ParseUnaryExpASTNode(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!stream.should
            .Expect("-")
            .ExpectConsumedBy(ParsePrimaryExpASTNode, out PrimaryExpASTNodeBase captured)
            .IsSatisfied
        ) { return ParseResult<UnaryExpASTNodeBase>.Failed("unaryExp.", "partialParseOneArg", pointer); }
        return new(
            new UnaryExpASTNode(captured),
            stream.CurrentPointer
        );

        throw new NotImplementedException();
    }
    public ParseResult<PrimaryExpASTNodeBase> ParsePrimaryExpASTNode(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        ScriptToken capturedToken;
        if (stream.maybe
            .ExpectVariable(out capturedToken)
            .IsSatisfied)
        {
            switch (capturedToken.type)
            {
                case ScriptToken.Type.INT_LITERAL:
                    return new(
                            new PrimaryExpASTNode(capturedToken.int_val),
                            stream.CurrentPointer
                        );
                case ScriptToken.Type.FLOAT_LITERAL:
                    return new(
                            new PrimaryExpASTNode(capturedToken.float_val),
                            stream.CurrentPointer
                        );
                case ScriptToken.Type.SYMBOL_ID:
                    return new(
                            new PrimaryExpASTNode(capturedToken.user_defined_symbol),
                            stream.CurrentPointer
                        );
            }
        }
        else if (stream.maybe
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase capturedExp)
            .IsSatisfied)
        {
            return new(
                    new PrimaryExpASTNode(capturedToken.user_defined_symbol),
                    stream.CurrentPointer
                );
        }
        return ParseResult<PrimaryExpASTNodeBase>.Failed("This token's line is not primary expression.", "PrimaryExpASTNode", pointer);
    }
    private bool TestCallFuncStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .ExpectSymbolID(out string _tmp)
            .Expect("(")
            .IsSatisfied;
    }
}
