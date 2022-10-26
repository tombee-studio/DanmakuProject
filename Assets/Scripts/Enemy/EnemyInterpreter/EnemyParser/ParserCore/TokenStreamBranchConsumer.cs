#nullable enable
using System;
public class TokenStreamBranchConsumer<ResultType> where ResultType : notnull
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
        try
        {
            var parseResult = parseFunction.Invoke(target.CurrentPointer);
            if (!parseResult.IsSucceeded()) return this;
            _result = parseResult.ParsedNode;
            parseResult.ApplyIfSucceeded(ref target);
            return this;
        }
        catch (ParseException e)  // should モードのときに投げられる exception を握りつぶす
        {
            return this;  // if (!parseResult.IsSucceeded()) return this;  と同じ
        }
    }
}
