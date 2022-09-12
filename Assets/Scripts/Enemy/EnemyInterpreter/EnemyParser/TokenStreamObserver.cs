using System;
using System.Collections.Generic;
using System.Linq;
public class TokenStreamObserver
{
    private static readonly Dictionary<ScriptToken.Type, string> reservedWords = EnemyLexer.mapFromTokenTypeToReservedWord;

    public class ParseException : Exception
    {
        public ParseException(string message, int pointer)
              : base(message + $" : at {pointer}"){}
    }



    
    public class TokenStreamValidation
    {
        private bool isValidated = true;
        private readonly bool isEnforced;
        private TokenStreamObserver observer;
        private TokenStream stream;
        public TokenStreamValidation(TokenStreamObserver observer, bool isEnforced)
        {
            this.isEnforced = isEnforced;
            this.observer = observer;
            stream = observer.stream;
        }

        private void recognizeValidationIsFailed(string errorMsg, int pointerTorelevantPlace = stream.pointer)
        {
            if (isEnforced) throw new ParseException(errorMsg, );
        }

        public TokenStreamValidation expectReserved(string expectedReservedToken)
        {
            var observedToken = stream.lookahead();

            if (!reservedWords.TryGetValue(observedToken.type, out string actualToken)) throw new Exception($"Given token `{observedToken}` is not reserved.");
            if (!expectedReservedToken.Equals(actualToken)) throw new ParseException($"Expected token is `{expectedReservedToken}`. However, `{actualToken}` is found.", stream.pointer);
            stream.proceed();
            return this;
        }
        public TokenStreamValidation expectVariable(out ScriptToken capturedToken)
        {
            var observedToken = stream.lookahead();
            ScriptToken.Type[] allowedToken = { ScriptToken.Type.INT_LITERAL, ScriptToken.Type.FLOAT_LITERAL, ScriptToken.Type.SYMBOL_ID };

            if (!allowedToken.Contains(observedToken.type)) throw new ParseException($"Variable token is expected. However, `{observedToken}` is found.", stream.pointer);
            capturedToken = observedToken;
            stream.proceed();
            return this;
        }
        public TokenStreamValidation expectSymbolID(out string capturedSymbolID)
        {
            var observedToken = stream.lookahead();

            if (observedToken.type != ScriptToken.Type.SYMBOL_ID) throw new ParseException($"Variable token is expected. However, `{observedToken}` is found.", stream.pointer);
            if (observedToken.user_defined_symbol == null) throw new Exception($"Variable token is coming, but it contains null as SymbolID.");
            capturedSymbolID = observedToken.user_defined_symbol;
            stream.proceed();
            return this;
        }
        // backtraceするときにどう状態を戻すか？
        // どこが試行を行い始めた時か？
        // 
        public TokenStreamValidation needRecipient<N>(Func<TokenStreamObserver, N> parser, string symbolName, out N parsedNode) where N:ASTNode
        {
            int pointerStartAt = stream.pointer;
            N parsedNodeNullable = parser.Invoke(observer);
            
            parsedNode = parsedNodeNullable ?? throw new ParseException($"${symbolName} is expected, However, ", stream.pointer);
        }
        
    }

    
    public TokenStream stream;
    public TokenStreamObserver(TokenStream tokenStream)
    {
        stream = tokenStream;
    }
    public TokenStreamValidation should()
    {
        return new TokenStreamValidation(stream, true);
    }
    public TokenStreamChecker maybe()
    {
    }


}
