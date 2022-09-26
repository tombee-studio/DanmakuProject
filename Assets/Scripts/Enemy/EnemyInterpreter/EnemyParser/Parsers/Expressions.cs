using System;
using System.Collections.Generic;
public partial class EnemyParser
{
    public ParseResult<CallFuncStASTNode> ParseCallFuncStASTNode(TokenStreamPointer pointer)
    {
        if (!TestCallFuncStASTNode(pointer))
            return ParseResult<CallFuncStASTNode>.Failed("This token's line is not function.", "CallFuncStASTNode", pointer);
        var stream = pointer.StartStream();
        stream.should
            .ExpectSymbolID(out string functionID)
            .Expect("(");

        var expList = new List<ExpASTNode>();
        while (
            stream.maybe
                .MaybeConsumedBy(ParseExpASTNode, out ExpASTNode expASTNode)
                .Maybe(",")
                .IsSatisfied
        ){ expList.Add(expASTNode); }

        stream.maybe
            .MaybeConsumedBy(ParseExpASTNode, out ExpASTNode expASTNode1);
        if (expASTNode1 is ExpASTNode) expList.Add(expASTNode1);

        return new(
                new CallFuncStASTNode(functionID, expList),
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
