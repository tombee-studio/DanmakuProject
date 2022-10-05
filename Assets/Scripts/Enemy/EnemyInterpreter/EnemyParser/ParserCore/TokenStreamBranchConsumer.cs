#nullable enable
using System;
public class TokenStreamBranchConsumer<ResultType> where ResultType:notnull
{
    public delegate ParseResult<NodeType> ParserFunction<NodeType>(TokenStreamPointer pointer) where NodeType : ResultType;

    private TokenStream target;
    private ResultType? _result;
    public ResultType? Result { get => _result; }

    public TokenStreamBranchConsumer(TokenStream _target)
    {
        target = _target;
    }
    public TokenStreamBranchConsumer<ResultType> Try<N>(ParserFunction<N> parseFunction) where N : ResultType
    {
        if (_result != null) return this;
        var parseResult = parseFunction.Invoke(target.CurrentPointer);
        if (!parseResult.IsSucceeded()) return this;
        _result = parseResult.ParsedNode;
        parseResult.ApplyIfSucceeded(ref target);
        return this;
    }
}
