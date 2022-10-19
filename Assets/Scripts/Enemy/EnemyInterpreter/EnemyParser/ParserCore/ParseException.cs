using System;
//TODO: エラー箇所を表示できるようにしたい
public class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
    public static ParseException Information(string message, TokenStreamPointer pointer) {
        ScriptToken currentToken = pointer.Access();
        return new(message + $" : at line {currentToken.LineNumber}, column {currentToken.ColumnNumber} token number {pointer.index}");
    }
}