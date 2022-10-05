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
    public ParseResult<EqualityExpASTNodeBase> ParseEqualityExpASTNode(TokenStreamPointer pointer)
    {
        var resultOfRelational = pointer.StartStream().should.ExpectConsumedBy(ParseRelationalExpASTNode, out var relational);
        if (resultOfRelational.CurrentPointer.OnTerminal()) return new(relational, resultOfRelational.CurrentPointer);
        var observerAfterRelational = resultOfRelational.CurrentPointer.StartStream();

        ScriptToken op = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        if (false) { }
        else if (observerAfterRelational.maybe.Expect("==").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.EQUAL);
        }
        else if (observerAfterRelational.maybe.Expect("!=").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.NOT_EQUAL);
        }
        else
        {
            return new(relational, observerAfterRelational.CurrentPointer);
        }
        var resultOFEquality = observerAfterRelational.should.ExpectConsumedBy(ParseEqualityExpASTNode, out var equality);
        return new(new EqualityExpASTNode(relational, op, equality), resultOFEquality.CurrentPointer);
    }
    public ParseResult<LogicalExpASTNodeBase> ParseLogicalExpASTNode(TokenStreamPointer pointer)
    {
        var resultOfEquality = pointer.StartStream().should.ExpectConsumedBy(ParseEqualityExpASTNode, out var equality);
        if (resultOfEquality.CurrentPointer.OnTerminal()) return new(equality, resultOfEquality.CurrentPointer);
        var observerAfterEquality = resultOfEquality.CurrentPointer.StartStream();

        ScriptToken op = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        if (false) { }
        else if (observerAfterEquality.maybe.Expect("and").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.AND);
        }
        else if (observerAfterEquality.maybe.Expect("or").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.OR);
        }
        else
        {
            return new(equality, observerAfterEquality.CurrentPointer);
        }
        var resultOFLogical = observerAfterEquality.should.ExpectConsumedBy(ParseLogicalExpASTNode, out var logical);
        return new(new LogicalExpASTNode(equality, op, logical), resultOFLogical.CurrentPointer);
    }
    public ParseResult<RelationalExpASTNodeBase> ParseRelationalExpASTNode(TokenStreamPointer pointer)
    {
        var resultOfTerm = pointer.StartStream().should.ExpectConsumedBy(ParseTermExpASTNode, out var term);
        if (resultOfTerm.CurrentPointer.OnTerminal()) return new(term, resultOfTerm.CurrentPointer);
        var observerAfterTerm = resultOfTerm.CurrentPointer.StartStream();

        ScriptToken op = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        if (false) { }
        else if (observerAfterTerm.maybe.Expect("<").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.LESS_THAN);
        }
        else if (observerAfterTerm.maybe.Expect(">").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_THAN);
        }
        else if (observerAfterTerm.maybe.Expect("<=").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.LESS_EQUAL);
        }
        else if (observerAfterTerm.maybe.Expect(">=").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.GREATER_EQUAL);
        }
        else
        {
            return new(term, observerAfterTerm.CurrentPointer);
        }
        var resultOFRelational = observerAfterTerm.should.ExpectConsumedBy(ParseRelationalExpASTNode, out var relational);
        return new(new RelationalExpASTNode(term, op, relational), resultOFRelational.CurrentPointer);
    }
    public ParseResult<TermExpASTNodeBase> ParseTermExpASTNode(TokenStreamPointer pointer)
    {
        var resultOfFactor = pointer.StartStream().should.ExpectConsumedBy(ParseFactorExpASTNode, out var factor);
        if (resultOfFactor.CurrentPointer.OnTerminal()) return new(factor, resultOfFactor.CurrentPointer);
        var observerAfterFactor = resultOfFactor.CurrentPointer.StartStream();

        ScriptToken op = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        if (false) { }
        else if (observerAfterFactor.maybe.Expect("+").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.PLUS);
        }
        else if (observerAfterFactor.maybe.Expect("-").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.SUB);
        }
        else
        {
            return new(factor, observerAfterFactor.CurrentPointer);
        }
        var resultOFTerm = observerAfterFactor.should.ExpectConsumedBy(ParseTermExpASTNode, out var term);
        return new(new TermExpASTNode(factor, op, term), resultOFTerm.CurrentPointer);
    }
    // FACTOR := UNARY | UNARY [*/%] FACTOR
    public ParseResult<FactorExpASTNodeBase> ParseFactorExpASTNode(TokenStreamPointer pointer)
    {
        var resultOfUnary = pointer.StartStream().should.ExpectConsumedBy(ParseUnaryExpASTNode, out var unary);
        // ExpectConsumedBy を使うとポインタが進まない.
        // これを解決するために, TokenStreamChecker.CurrentPointer として TokenStreamChecker.target.CurrentPointer を読み込めるようにした.
        // この TokenStreamChecker.CurrentPointer を用いて新たな observer を生成する. (1)

        // ただし, observer を生成する段階で pointer が OnTerminal の場合はエラーを吐くので事前に弾いておく.
        if (resultOfUnary.CurrentPointer.OnTerminal()) return new(unary, resultOfUnary.CurrentPointer);
        var observerAfterUnary = resultOfUnary.CurrentPointer.StartStream();  // (1)

        ScriptToken op = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        if (false) { }
        else if (observerAfterUnary.maybe.Expect("*").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.MULTIPLY);
        }
        else if (observerAfterUnary.maybe.Expect("/").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.DIVIDE);
        }
        else if (observerAfterUnary.maybe.Expect("%").IsSatisfied)
        {
            op = ScriptToken.GenerateToken("", ScriptToken.Type.MOD);
        }
        else
        {
            return new(unary, observerAfterUnary.CurrentPointer);
        }
        // 上記と同じく result を受け取って更新後の CurrentPointer を受け取れるようにする
        var resultOFFactor = observerAfterUnary.should.ExpectConsumedBy(ParseFactorExpASTNode, out var factor);
        return new(new FactorExpASTNode(unary, op, factor), resultOFFactor.CurrentPointer);
    }
    public ParseResult<UnaryExpASTNodeBase> ParseUnaryExpASTNode(TokenStreamPointer pointer)
    {
        var observer = pointer.StartStream();
        ScriptToken sign;
        if(false){}
        else if(observer.maybe.Expect("-").IsSatisfied){
            sign = ScriptToken.GenerateToken("", ScriptToken.Type.SUB);
        }
        else if(observer.maybe.Expect("+").IsSatisfied){
            sign = ScriptToken.GenerateToken("", ScriptToken.Type.PLUS);
        }
        else if(observer.maybe.Expect("not").IsSatisfied){
            sign = ScriptToken.GenerateToken("", ScriptToken.Type.NOT);
        }
        else{
            sign = ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        }
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
        TokenStreamChecker result = observer.maybe.ExpectVariable(out ScriptToken capturedToken);
        TokenStreamChecker resultOfExp;
        if (result.IsSatisfied)
        {
            switch (capturedToken.type)
            {
                case ScriptToken.Type.INT_LITERAL:
                    return new(new PrimaryExpASTNode(capturedToken.int_val), result.CurrentPointer);
                case ScriptToken.Type.FLOAT_LITERAL:
                    return new(new PrimaryExpASTNode(capturedToken.float_val), result.CurrentPointer);
                case ScriptToken.Type.SYMBOL_ID:
                    return new(new PrimaryExpASTNode(capturedToken.user_defined_symbol), result.CurrentPointer);
                default:
                    throw new Exception($"Unexpected token {capturedToken.ToString()} received");
            }
        }
        else if ((resultOfExp = observer.maybe
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase capturedExp)
            ).IsSatisfied)
        {
            return new(new PrimaryExpASTNode(capturedToken.user_defined_symbol), resultOfExp.CurrentPointer);
        }
        return ParseResult<PrimaryExpASTNodeBase>.Failed("This token's line is not primary expression.", "PrimaryExpASTNode", pointer);
    }
}
