using System;
using System.Collections.Generic;

public class TokenStream
{
    private readonly List<ScriptToken> sequence;
    private int index;

    public TokenStream(List<ScriptToken> target, int pointer = 0)
    {
        sequence = target;
        index = pointer;
    }

    public static TokenStream FromPointer(TokenStreamPointer pointer) => new(pointer.sequence, pointer.index);
    public TokenStreamPointer CurrentPointer => new(sequence, index);

    public void RestartStreamFrom(TokenStreamPointer p)
    {
        if (!this.sequence.Equals(p.sequence)) throw new Exception("Can not restart on the different token sequence.");
        index = p.index;
    }

    public ScriptToken Read()
    {
        if (CurrentPointer.OnTerminal()) return ScriptToken.EndOfFile;
        return sequence[index++];
    }
    public ScriptToken Lookahead() => sequence[index];

    public TokenStreamChecker should => new(this, true, index);
    public TokenStreamChecker maybe => new(this, false, index);
    public void RecoverPointer(int initialPointer)
    {
        this.index = initialPointer;
    }
    public TokenStreamBranch<BasedNodeType> match<BasedNodeType>()
    {
        return new TokenStreamBranch<BasedNodeType>(this);
    }

}
