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
    public ParseResult<CallFuncStASTNode> ParseCallFuncStASTNode(TokenStreamPointer pointer)
    {
        if (!TestCallFuncStASTNode(pointer))
            return ParseResult<CallFuncStASTNode>.Failed("This token's line is not function.", "CallFuncStASTNode", pointer);
        var stream = pointer.StartStream();
        stream.should
            .ExpectSymbolID(out string functionID)
            .Expect("(")
            .ExpectMultiComsumer(partialParseOneArg, out List<ExpASTNode> expASTNodes)
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
    private ParseResult<ExpASTNode> partialParseOneArg(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (
            !stream.maybe
                .ExpectConsumedBy(ParseExpASTNode, out var exp)
                .Expect(",")
                .IsSatisfied
        ) { return ParseResult<ExpASTNode>.Failed("Arg sequence is finished.", "partialParseOneArg", pointer); }
        return new(
                exp,
                stream.CurrentPointer
        );
    }

    public ParseResult<ExpASTNode> ParseExpASTNode(TokenStreamPointer pointer)
    {
        throw new NotImplementedException();
    }
    private bool TestCallFuncStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .ExpectSymbolID(out string _tmp)
            .Expect("(")
            .IsSatisfied;
        
    }
}
