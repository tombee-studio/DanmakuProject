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
}
