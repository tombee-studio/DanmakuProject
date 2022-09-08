using System;
public partial class EnemyLexerTester
{
    public void test_invalidLexedCodeError()
    {
        // 本来はint 52と打つべき
        assertSequenceThrow<InvalidTokenSequenceException>(@"", @"52");
    }
    public void test_unmatchedLiteralTypeError()
    {
        assertSequenceThrow<TokenTypeMismatchedException>(@"52", @"float 52f");
        assertSequenceThrow<TokenTypeMismatchedException>(@"52f", @"int 52");
    }
    public void test_unmatchedLiteralValueError()
    {
        assertSequenceThrow<TokenValueMismatchedException>(@"51", @"int 52");
        assertSequenceThrow<TokenValueMismatchedException>(@"52f", @"int 52");
    }
}
