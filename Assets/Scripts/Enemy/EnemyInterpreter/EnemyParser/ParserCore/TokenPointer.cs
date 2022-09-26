using System;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class TokenStreamPointer
{
    public readonly List<ScriptToken> sequence;
    // TokenStream中のポインタを表す。`0 <= index <= sequence.Count`を満たす。
    // index == sequence.Countの時はsequence外にあるポインタとなるが、
    // このときこのポインタがTokenStreamを読み終えたものであることを表す。これを終端ポインタと呼ぶことにする。
    public readonly int index;
    public TokenStreamPointer(List<ScriptToken> target, int pointer = 0)
    {
        sequence = target;
        if (pointer < 0 || sequence.Count < pointer) throw ParseException.Information("Can not make TokenPointer pointing to the area out of sequence.", this);
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
    /// <summary>
    /// このポインタが終端ポインタであるか否かを返します。
    /// </summary>
    public bool OnTerminal()
    {
        return sequence.Count == index; 
    }
    public TokenStreamPointer GetNextPointer()
    {
        if (OnTerminal()) throw new Exception("This Pointer is on the terminal. Therefore you can't proceed with the pointer any longer.");
        return new TokenStreamPointer(sequence, index + 1);
    }
    public TokenStream StartStream()
    {
        if (OnTerminal()) throw new Exception("This Pointer is on the terminal. Therefore you can't proceed with the pointer any longer.");
        return new TokenStream(sequence, index);
    }
    
}
