using System;
public partial class EnemyParser
{
    public ParseResult<CallFuncStASTNode> ParseCallFuncStASTNode(TokenStreamPointer pointer)
    {
        if (!TestCallFuncStASTNode(pointer))
            return ParseResult<CallFuncStASTNode>.Failed("This token's line is not function.", "CallFuncStASTNode", pointer);
        var stream = pointer.StartStream();
        stream.should
            .ExpectSymbolID(out string _tmp)

    }
    private bool TestCallFuncStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .ExpectSymbolID(out string _tmp)
            .Expect("(")
            .IsSatisfied;
        
    }
}
