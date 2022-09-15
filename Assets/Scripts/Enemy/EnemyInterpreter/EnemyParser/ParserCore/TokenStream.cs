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
    public TokenStreamPointer CurrentPointer =>  new(sequence, index);


    public ScriptToken Read() => sequence[index++];
    public ScriptToken Lookahead() => sequence[index];

    public TokenStreamChecker should => new(this, true);
    public TokenStreamChecker maybe => new(this, false);

}
