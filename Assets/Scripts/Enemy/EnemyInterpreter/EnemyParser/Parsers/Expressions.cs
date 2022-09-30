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
        var resultOfFactor = pointer.StartStream().should.ExpectConsumedBy(ParseFactorExpASTNode, out var factor);
        if(resultOfFactor.CurrentPointer.OnTerminal()) return new(factor, resultOfFactor.CurrentPointer);
        var observerAfterFactor = resultOfFactor.CurrentPointer.StartStream();  // (1)

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
        if(resultOfUnary.CurrentPointer.OnTerminal()) return new(unary, resultOfUnary.CurrentPointer);
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
        var sign = (observer.maybe.Expect("-").IsSatisfied)
            ? ScriptToken.GenerateToken("", ScriptToken.Type.SUB)
            : ScriptToken.GenerateToken("", ScriptToken.Type.NONE);
        var res = observer.should.ExpectConsumedBy(ParsePrimaryExpASTNode, out PrimaryExpASTNodeBase captured);
        return new(
            new UnaryExpASTNode(sign, captured),
            res.CurrentPointer
        );
    }
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
