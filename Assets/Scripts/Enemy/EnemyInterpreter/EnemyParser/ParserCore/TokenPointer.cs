using System.Collections.Generic;
/// <summary>
/// Immutable.
/// </summary>
public class TokenStreamPointer
{
    public readonly List<ScriptToken> sequence;
    public readonly int index;
    public TokenStreamPointer(List<ScriptToken> target, int pointer = 0)
    {
        sequence = target;
        if (pointer + 1 > sequence.Count) throw ParseException.Information("Can not make TokenPointer pointing to the area out of sequence.", this);
        index = pointer;
    }
    /// <summary>
    /// 先読みを行う。
    /// </summary>
    /// <param name="delta">現在の位置からどれだけ先のトークンを読むか</param>
    /// <returns></returns>
    public ScriptToken Access(int delta = 0)
    {
        var actualPointer = index + delta;
        if (actualPointer > sequence.Count) throw ParseException.Information($"try to access ${actualPointer} but failed because the token sequence's length is {sequence.Count}.", this);
        return sequence[actualPointer];
    }

    public TokenStreamPointer GetNextPointer()
    {
        return new TokenStreamPointer(sequence, index + 1);
    }
    public TokenStream StartStream()
    {
        return new TokenStream(sequence, index);
    }
    
}
