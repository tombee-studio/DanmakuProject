using System;
//TODO: エラー箇所を表示できるようにしたい
public class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
    public static ParseException Information(string message, TokenStreamPointer pointer) {
        TokenRangeInCode range = pointer.Access().range;
        int endColumnNumber = range.length + range.beginColumnNumber;
        return new(message + $" : at line {range.lineNumber}, column {range.beginColumnNumber} to {endColumnNumber} token number {pointer.index}");
    }
}