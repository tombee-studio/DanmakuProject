using System;
public partial class EnemyLexerTester
{
    public void test_easy1()
    {
        assertIsSequenceEqual(@"(", @"(");

    }
    
    public void test_easy2()
    {
        assertIsSequenceEqual(@"+", @"+");
    }
    public void test_easy3()
    {
        assertIsSequenceEqual(@"1", @"int 1");
    }
    
    public void test_easy4()
    {
        assertIsSequenceEqual(@"1.23f", @"float 1.23f");
    }
    

    public void test_simpleExpression1()
    {
        assertIsSequenceEqual(
            "(1 + 2)",

            @"
            (
            int 1
            +
            int 2
            )
            "
        );
    }
    public void test_simpleExpression2()
    {
        assertIsSequenceEqual(
            "(1f + 2 * 3)",

            @"
            (
            float 1.0f
            +
            int 2
            *
            int 3
            )
            "
        );
    }
    public void test_includingNewLine()
    {
        assertIsSequenceEqual(
            @"(1f +
            2 * 3)",

            @"
            (
            float 1.0f
            +
            int 2
            *
            int 3
            )
            "
        );
    }
    public void test_symbolID()
    {
        assertIsSequenceEqual(
            "variable + data",

            @"
            symbolID variable
            +
            symbolID data
            "
        );
    }

    public void test_invalidLexedCodeError()
    {
        assertSequenceThrow<InvalidTokenSequenceException>(@"", @"52");
        assertSequenceThrow<InvalidTokenSequenceException>(@"", @"float 0.1352");
    }


    public void test_easy_error()
    {
        assertSequenceThrow<TokenTypeMismatchedException>(@"+", @"-");
    }

    public void test_easy3_error()
    {
        assertSequenceThrow<TokenValueMismatchedException>(
            @"1", @"int 3"
        );
        assertSequenceThrow<TokenValueMismatchedException>(
            @"3", @"int 1"
        );
    }
    public void test_throw_on_invalidCode()
    {
        assertSequenceThrow<EnemyLexer.InvalidTokenException>(@"0.1312", @"float 0.1312f");

    }
}
