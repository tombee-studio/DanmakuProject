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
        return ParseFactorASTNode(pointer);
    }

    public ParseResult<ExpASTNodeBase> ParseFactorASTNode(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        stream.should
            .ExpectVariable(out ScriptToken token);
        return new ParseResult<ExpASTNodeBase>(
            new PrimaryExpASTNode(token.int_val),
            stream.CurrentPointer);
    }

    private bool TestCallFuncStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .ExpectSymbolID(out string _tmp)
            .Expect("(")
            .IsSatisfied;
        
    }
}
