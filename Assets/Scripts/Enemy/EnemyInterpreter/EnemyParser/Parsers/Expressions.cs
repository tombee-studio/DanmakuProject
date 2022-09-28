#nullable enable
using System.Net.Http;
using System.IO;
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
        var observer = pointer.StartStream();
        observer.should
            .ExpectSymbolID(out string functionID)
            .Expect("(")
            .ExpectMultiComsumer(partialParseOneArg, out List<ExpASTNodeBase> expASTNodes)
            .MaybeConsumedBy(ParseExpASTNode, out var expASTNodeNullable);
        if (expASTNodeNullable != null) expASTNodes.Add(expASTNodeNullable);

        return new(
                new CallFuncStASTNode(functionID, expASTNodes),
                observer.CurrentPointer
        );
    }
    private bool TestCallFuncStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .ExpectSymbolID(out string _tmp)
            .Expect("(")
            .IsSatisfied;
    }

    /**
     * (EXP,) の部分
     */
    private ParseResult<ExpASTNodeBase> partialParseOneArg(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        if (
            !observer.maybe
                .ExpectConsumedBy(ParseExpASTNode, out var exp)
                .Expect(",")
                .IsSatisfied
        ) { return ParseResult<ExpASTNodeBase>.Failed("Arg sequence is finished.", "partialParseOneArg", pointer); }
        return new(
                exp,
                observer.CurrentPointer
        );
    }

    public ParseResult<ExpASTNodeBase> ParseExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        return new(
            ParseEqualityExpASTNode(pointer).ParsedNode,
            observer.CurrentPointer
        );
    }
    public ParseResult<EqualityExpASTNodeBase> ParseEqualityExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        return new(
            ParseRelationalExpASTNode(pointer).ParsedNode,
            observer.CurrentPointer
        );
    }
    public ParseResult<RelationalExpASTNodeBase> ParseRelationalExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        return new(
            ParseTermExpASTNode(pointer).ParsedNode,
            observer.CurrentPointer
        );
    }
    public ParseResult<TermExpASTNodeBase> ParseTermExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        return new(
            ParseFactorExpASTNode(pointer).ParsedNode,
            observer.CurrentPointer
        );
    }
    public ParseResult<FactorExpASTNodeBase> ParseFactorExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        return new(
            ParseUnaryExpASTNode(pointer).ParsedNode,
            observer.CurrentPointer
        );
    }
    public ParseResult<UnaryExpASTNodeBase> ParseUnaryExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        var sign = (observer.maybe.Expect("-").IsSatisfied)
        ? ScriptToken.GenerateToken("", ScriptToken.Type.SUB)
        : ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        observer.should.ExpectConsumedBy(ParsePrimaryExpASTNode, out PrimaryExpASTNodeBase captured);
        return new(
            new UnaryExpASTNode(sign, captured),
            observer.CurrentPointer
        );
    }
    public ParseResult<PrimaryExpASTNodeBase> ParsePrimaryExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        if (observer.maybe
            .ExpectVariable(out ScriptToken capturedToken)
            .IsSatisfied)
        {
            switch (capturedToken.type)
            {
                case ScriptToken.Type.INT_LITERAL:
                    return new(new PrimaryExpASTNode(capturedToken.int_val), observer.CurrentPointer);
                case ScriptToken.Type.FLOAT_LITERAL:
                    return new(new PrimaryExpASTNode(capturedToken.float_val), observer.CurrentPointer);
                case ScriptToken.Type.SYMBOL_ID:
                    return new(new PrimaryExpASTNode(capturedToken.user_defined_symbol), observer.CurrentPointer);
                default:
                    throw new Exception($"Unexpected token {capturedToken.ToString()} received");
            }
        }
        else if (observer.maybe
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase capturedExp)
            .IsSatisfied)
        {
            return new(new PrimaryExpASTNode(capturedToken.user_defined_symbol), observer.CurrentPointer);
        }
        return ParseResult<PrimaryExpASTNodeBase>.Failed("This token's line is not primary expression.", "PrimaryExpASTNode", pointer);
    }
}
