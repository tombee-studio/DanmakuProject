#nullable enable
using System.Net.Http;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        return new(ParseEqualityExpASTNode(pointer).ParsedNode, pointer);
    }
    public ParseResult<LogicalExpASTNodeBase> ParseLogicalExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        observer.should
            .ExpectConsumedBy(ParseEqualityExpASTNode, out var equality);
        var result = observer.match()
            .Try("and")
            .Try("or");

        if (result.Result == null)
        {
            return new(equality, observer.CurrentPointer);
        }
        observer.should.ExpectConsumedBy(ParseLogicalExpASTNode, out var logical);
        return new(new LogicalExpASTNode(equality, (ScriptToken)result.Result, logical), observer.CurrentPointer);
    }
    public ParseResult<EqualityExpASTNodeBase> ParseEqualityExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        observer.should
            .ExpectConsumedBy(ParseRelationalExpASTNode, out var relational);

        var result = observer.match()
            .Try("==")
            .Try("!=");

        if (result.Result == null)
        {
            return new(relational, observer.CurrentPointer);
        }
        observer.should.ExpectConsumedBy(ParseEqualityExpASTNode, out var equality);
        return new(new EqualityExpASTNode(relational, (ScriptToken)result.Result, equality), observer.CurrentPointer);
    }
    public ParseResult<RelationalExpASTNodeBase> ParseRelationalExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        observer.should
            .ExpectConsumedBy(ParseTermExpASTNode, out var term);
        var result = observer.match()
            .Try("<")
            .Try(">")
            .Try("<=")
            .Try(">=");
        if (result.Result == null)
        {
            return new(term, observer.CurrentPointer);
        }
        observer.should.ExpectConsumedBy(ParseRelationalExpASTNode, out var relational);
        return new(new RelationalExpASTNode(term, (ScriptToken)result.Result, relational), observer.CurrentPointer);
    }
    public ParseResult<TermExpASTNodeBase> ParseTermExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        observer.should
            .ExpectConsumedBy(ParseFactorExpASTNode, out var factor);
        var result = observer.match()
            .Try("+")
            .Try("-");
        if (result.Result == null)
        {
            return new(factor, observer.CurrentPointer);
        }
        observer.should.ExpectConsumedBy(ParseTermExpASTNode, out var term);
        return new(new TermExpASTNode(factor, (ScriptToken)result.Result, term), observer.CurrentPointer);
    }
    // FACTOR := UNARY | UNARY [*/%] FACTOR
    public ParseResult<FactorExpASTNodeBase> ParseFactorExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        observer.should.ExpectConsumedBy(ParseUnaryExpASTNode, out var unary);
        var result = observer.match()
            .Try("*")
            .Try("/")
            .Try("%");
        var op = result.Result;
        if (op == null)
        {
            return new(unary, observer.CurrentPointer);
        }
        observer.should.ExpectConsumedBy(ParseFactorExpASTNode, out var factor);
        return new(new FactorExpASTNode(unary, (ScriptToken)op, factor), observer.CurrentPointer);
    }
    public ParseResult<UnaryExpASTNodeBase> ParseUnaryExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        ScriptToken sign;
        var result = observer.match()
            .Try("-")
            .Try("+")
            .Try("not");
        if (result.Result == null) sign = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        else sign = (ScriptToken)result.Result;
        var res = observer.should.ExpectConsumedBy(ParsePrimaryExpASTNode, out PrimaryExpASTNodeBase captured);
        return new(
            new UnaryExpASTNode(sign, captured),
            res.CurrentPointer
        );
    }

    private bool TestParseUnaryExpASTNode(TokenStreamPointer pointer)
        => pointer.StartStream().maybe
            .Expect("-")
            .ExpectConsumedBy(ParsePrimaryExpASTNode, out PrimaryExpASTNodeBase captured)
            .IsSatisfied;

    public ParseResult<PrimaryExpASTNodeBase> ParsePrimaryExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        if (observer.maybe
            .ExpectVariable(out var capturedToken)
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
        else
        {
            observer.should
                .Expect("(");
            observer.should.ExpectConsumedBy(ParseExpASTNode, out var exp);
            observer.should.Expect(")");
            return new(new PrimaryExpASTNode(exp), observer.CurrentPointer);
        }
    }
}
