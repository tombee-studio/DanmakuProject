using System;
public class EnemyFunctionFactory
{
    private EnemyFunctionFactory(){}

    private static EnemyFunctionFactory _singleton;
    private static EnemyFunctionFactory singleton {
        get => _singleton ??= new EnemyFunctionFactory();
    }

    public static EnemyFunctionFactory GetInstance() => _singleton;

    public void Call(
        int functionCode,
        EnemyComponent enemyComponent,
        EnemyVM enemyVM
    ){
        throw new Exception("Not implemented.");
    }
}
