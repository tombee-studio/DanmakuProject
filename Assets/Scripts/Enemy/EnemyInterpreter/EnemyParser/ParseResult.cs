using System;

public struct ParseResult<N>
{
    enum State
    {
        Succeeded, Failed
    };
    private State _state;
    private N _parsedNode;
    private TokenPointer _pointWhenParseCompleted;


    public bool IsSucceeded()
    {
        return _state == State.Succeeded;
    }
    public N ParsedNode
    {
        get => IsSucceeded() ? _parsedNode : throw new Exception("This ParseResult is not in Succeeded state, so any result is not contained in this object.");
    }
    public TokenPointer pointer
    {
        get => IsSucceeded() ? _pointWhenParseCompleted : throw new Exception("This ParseResult is not in Succeeded state, so any result is not contained in this object.");
    }


    public ParseResult(N parsedNode, TokenPointer currentPointer)
    {
        _state = State.Succeeded;
        _parsedNode = parsedNode;
        _pointWhenParseCompleted = currentPointer;
    }
    public static ParseResult<N> Failed() => new() { _state = State.Failed };
}
