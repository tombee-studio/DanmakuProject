#nullable enable
using System;
public class TokenStreamBranch
{
    private TokenStream target;
    private ScriptToken? _result;
    public ScriptToken? Result { get => _result; }

    public TokenStreamBranch(TokenStream _target)
    {
        target = _target;
    }
    public TokenStreamBranch Try(string tokenInString)
    {
        if (_result != null) return this;
        var parseResult = target.maybe.Expect(tokenInString);
        if (parseResult.IsSatisfied)
        {
            _result = target.CurrentPointer.sequence[target.CurrentPointer.index - 1];
        }
        return this;
    }
}
