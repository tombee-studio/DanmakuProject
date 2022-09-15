#nullable enable
using System;


public struct ParseResult<N> where N:notnull
{
    public enum State
    {
        Succeeded, Failed
    };
    private readonly State _state;
    private readonly string? failedReason;
    private readonly string? parserName;
    private readonly N? _parsed;
    private readonly TokenStreamPointer _pointerWhenParseFinished;    


    public bool IsSucceeded()
    {
        return _state == State.Succeeded;
    }
    public N ParsedNode => _parsed ??
        throw new Exception("This ParseResult is not in Succeeded state, so any result is not contained in this object.");

    public N? ParsedNodeNullable => _parsed;

    public TokenStreamPointer pointerWhenParseFinished
        =>  _pointerWhenParseFinished ??
        throw new Exception("This ParseResult is not in Succeeded state, so any result is not contained in this object.");

    public ParseResult(State state, string? failedReason, string parserName, N? parsedNode, TokenStreamPointer currentPointer)
    {
        _state = state;
        this.parserName = parserName;
        this.failedReason = failedReason;
        _parsed = parsedNode;
        _pointerWhenParseFinished = currentPointer;
    }

    public ParseResult(N parsedNode, TokenStreamPointer currentPointer)
    {
        _state = State.Succeeded;
        failedReason = null;
        parserName = null;
        _parsed = parsedNode;
        _pointerWhenParseFinished = currentPointer;
    }

    public ParseResult<N> ApplyIfSucceeded(ref TokenStream stream)
    {
        if (IsSucceeded()) stream = TokenStream.FromPointer(pointerWhenParseFinished);
        return this;
    }
    public ParseResult<N> ShouldSucceed()
    {
        if (!IsSucceeded()) throw ParseException.Information($"The parse {parserName} should succeed but it's failed.\nreason: {failedReason}", pointerWhenParseFinished);
        return this;
    }
    public static ParseResult<N> Failed(string reason, string parserName, TokenStreamPointer position)
    {
        var Null = default(N);
        return new(State.Failed, reason, parserName, Null, position);
    }
}
