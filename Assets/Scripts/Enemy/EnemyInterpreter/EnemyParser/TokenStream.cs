using System;
using System.Collections.Generic;
public class TokenStream
{
    readonly List<ScriptToken> tokenSequence;
    int _pointer = 0;
    public int pointer { get => _pointer; }

    public TokenStream(List<ScriptToken> tokens)
    {
        tokenSequence = tokens;
    }
    public ScriptToken proceed()
    {
        // 後置インクリメントの性質より、返されるのはtokenSequence[_pointer]
        return tokenSequence[_pointer++];
    }
    public ScriptToken lookahead()
    {
        return tokenSequence[_pointer];
    }
}
