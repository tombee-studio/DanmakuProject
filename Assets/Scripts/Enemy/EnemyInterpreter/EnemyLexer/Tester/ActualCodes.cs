using System;
public partial class EnemyLexerTester
{
    void test_actualCode1()
    {
        assertIsSequenceEqual(@"
        behavior enemy001 {
          bullet >>
            ID: 0
              set_in_circle(0, 10, 20...)
              delay(10)
              accel(60, 1/100)
          action >>
            float x
            int i
            x = 100
            i = 0
            move(10, -10)
            repeat(10) {
              on_bullet(10)
              if(x) break
              if(x) {
                on_fire(0)
              } else {
                move(10, -10)
              }
            }
        }
    ", @"
        behavior
        symbolID enemy001
        {

        bullet
        >>

        ID
        :

        int 0

        symbolID set_in_circle
        (
        int 0
        ,
        int 10
        ,
        int 20
        )

        symbolID delay
        (
        int 10
        )
        symbolID accel
        (
        int 60
        ,
        int 1
        /
        int 100
        )

        action
        >>

        float
        symbolID x

        int
        symbolID i

        symbolID x
        =
        int 100

        symbolID i
        =
        int 0

        symbolID move
        (
        int 10
        ,
        -
        int 10
        )

        repeat
        (
        int 10
        )
        {

        symbolID on_bullet
        (
        int 10
        )

        if
        (
        symbolID x
        )
        break

        if
        (
        symbolID x
        )
        {

        symbolID on_fire
        (
        int 0
        )

        }

        else
        {

        symbolID move
        (
        int 10
        ,
        -
        int 10
        )

        }
        }
        }
        ");
    }
}