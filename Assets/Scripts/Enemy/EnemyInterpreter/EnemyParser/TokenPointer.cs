using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Immutable.
/// </summary>
public struct TokenPointer
{
    public static Dictionary<ScriptToken.Type, string> reservedWordMap = EnemyLexer.mapFromTokenTypeToReservedWord;
    private static string convertToString(ScriptToken.Type type)
    {
        if (reservedWordMap.TryGetValue(type, out string reservedWords)) throw new Exception($"The type `{type}` is not defined as Token.");
        return reservedWords;
    }

    readonly List<ScriptToken> sequence;
    readonly int pointer;
    public TokenPointer(List<ScriptToken> target, int pointer = 0)
    {
        sequence = target;
        if (pointer + 1 > sequence.Count) throw ParseException.Information("Can not make TokenPointer pointing to the area out of sequence.", pointer);
        this.pointer = pointer;
    }
    /// <summary>
    /// 先読みを行う。
    /// </summary>
    /// <param name="delta">現在の位置からどれだけ先のトークンを読むか</param>
    /// <returns></returns>
    public ScriptToken Access(int delta = 0)
    {
        var actualPointer = pointer + delta;
        if (actualPointer > sequence.Count) throw ParseException.Information($"try to access ${actualPointer} but failed because the token sequence's length is {sequence.Count}.", actualPointer);
        return sequence[actualPointer];
    }
    public TokenPointer GetNextPointer()
    {
        
        return new TokenPointer(sequence, pointer + 1);
    }
    public TokenPointer Expect(string tokenInString)
    {
        var nextToken = Access();
        if (!convertToString(nextToken.type).Equals(tokenInString))
            throw ParseException.Information($"expected `{tokenInString}` but `{convertToString(nextToken.type)}` is coming.", pointer);
        return GetNextPointer();
    }
    public TokenPointer captureSymbolID(out string captured)
    {
        var nextToken = Access();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID)
            throw ParseException.Information($"expected something of symbolID but `{nextToken.type}` is coming.", pointer);
        captured = nextToken.user_defined_symbol;
        return GetNextPointer();
    }
    public TokenPointer captureVariableToken(out ScriptToken captured)
    {
        var nextToken = Access();
        ScriptToken.Type[] allowedTokenTypeList = {
            ScriptToken.Type.SYMBOL_ID,
            ScriptToken.Type.INT_LITERAL,
            ScriptToken.Type.FLOAT_LITERAL
        };
        if (!allowedTokenTypeList.Contains(nextToken.type))
            throw ParseException.Information($"expected something of variable tokens but `{nextToken.type}` is coming.", pointer);
        captured = nextToken;
        return GetNextPointer();
    }


    public TokenPointer Maybe(string tokenInString)
    {
        var nextToken = Access();
        if (convertToString(nextToken.type).Equals(tokenInString)) return GetNextPointer();
        return this;
    }
    public TokenPointer MaybeSymbolID(out string captured)
    {
        var nextToken = Access();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID)
            throw ParseException.Information($"expected something of symbolID but `{nextToken.type}` is coming.", pointer);
        captured = nextToken.user_defined_symbol;
        return GetNextPointer();
    }

    public ParseResult<string> TryCaptureSymbolID()
    {
        var nextToken = Access();
        if (nextToken.type != ScriptToken.Type.SYMBOL_ID) return ParseResult<string>.Failed();
        return new ParseResult<string>(nextToken.user_defined_symbol, GetNextPointer());
    }
    public ParseResult<ScriptToken> TryCaptureVariableToken()
    {
        var nextToken = Access();
        ScriptToken.Type[] allowedTokenTypeList = {
            ScriptToken.Type.SYMBOL_ID,
            ScriptToken.Type.INT_LITERAL,
            ScriptToken.Type.FLOAT_LITERAL
        };
        if (!allowedTokenTypeList.Contains(nextToken.type)) return ParseResult<ScriptToken>.Failed();
        return new ParseResult<ScriptToken>(nextToken, GetNextPointer());
    }
}

//TODO: エラー箇所を表示できるようにしたい
public class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
    public static ParseException Information(string message, int pointer)
        => new(message + $" : at token number {pointer}");
}