public partial class EnemyLexer
{
    public struct Token
    {
        public TokenType type;
        public string user_defined_symbol;
        public int int_val;
        public float float_val;

        public static Token GenerateReservedToken(TokenType giventype)
        {
            var token = new Token();
            token.type = giventype;
            return token;
        }
        public static Token GenerateUserDefinedSymbolToken(string given_user_defined_symbol)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.user_defined_symbol = given_user_defined_symbol;
            return token;
        }
        private static Token GenerateIntValToken(string given_int_val)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.int_val = int.Parse(given_int_val);
            return token;
        }
        private static Token GenerateFloatValToken(string given_float_val)
        {
            var token = new Token();
            token.type = TokenType.USER_DEFINED_SYMBOL;
            token.float_val = float.Parse(given_float_val);
            return token;
        }
        /**
         * 与えられたトークンの情報から適切にトークンを生成する。
         */
        public static Token generateToken(string snippet, TokenType token)
        {
            switch (token)
            {
                case TokenType.USER_DEFINED_SYMBOL:
                    return GenerateUserDefinedSymbolToken(snippet);
                case TokenType.INT_LITERAL:
                    return GenerateIntValToken(snippet);
                case TokenType.FLOAT_LITERAL:
                    return GenerateIntValToken(snippet);
                default:
                    return GenerateReservedToken(token);
            }
        }
    };



}