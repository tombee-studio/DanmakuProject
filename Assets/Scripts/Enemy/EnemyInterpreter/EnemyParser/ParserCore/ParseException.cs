using System;
//TODO: エラー箇所を表示できるようにしたい
public class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
    public static ParseException Information(string message, TokenStreamPointer pointer)
        => new(message + $" : at token number {pointer.index}");
}