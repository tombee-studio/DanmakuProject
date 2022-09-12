using System;
public partial class EnemyLexerTester
{
    public class NoMatchException : Exception
    {
        public NoMatchException(string message) : base(message) { }
    }

    public class NoException : Exception
    {
        public NoException()
            : base("No exception was thrown.") { }
    }

    public class InvalidTokenSequenceException : Exception
    {
        public InvalidTokenSequenceException(string message) : base(message) { }
    }

    public class TokenTypeMismatchedException : Exception
    {
        public TokenTypeMismatchedException(string message) : base(message) { }
    }

    public class TokenValueMismatchedException : Exception
    {
        public TokenValueMismatchedException(ScriptToken result, ScriptToken expected)
            : base($"expected {expected.user_defined_symbol}, {expected.int_val}, {expected.float_val}  -- result {result.user_defined_symbol}, {result.int_val}, {expected.float_val}") { }
    }
}
