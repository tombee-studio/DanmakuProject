using System;
using System.Reflection;

public class EnemyFunctionFactory
{
    private MethodInfo[] functions;

    private EnemyFunctionFactory() {
        BindingFlags flag = BindingFlags.NonPublic;
        functions = GetType().GetMethods(flag);
    }

    private static EnemyFunctionFactory _singleton;
    private static EnemyFunctionFactory singleton {
        get => _singleton ??= new EnemyFunctionFactory();
    }

    private static string GetSnakeCase(string str)
    {
        var regex = new System.Text.RegularExpressions.Regex("[a-z][A-Z]");
        return regex.Replace(str, s => $"{s.Groups[0].Value[0]}_{s.Groups[0].Value[1]}").ToUpper();
    }

    public static EnemyFunctionFactory GetInstance() =>
        _singleton ??= new EnemyFunctionFactory();

    public void Call(
        int functionCode,
        EnemyComponent enemyComponent,
        EnemyVM enemyVM
    ){
        BindingFlags flag = BindingFlags.NonPublic;
        object[] args = new object[] { enemyComponent, enemyVM };
        GetType().GetMethods(flag)[functionCode].Invoke(this, args);
    }

    public int Find(String functionName) =>
        Array.FindIndex(functions,
            item => EnemyFunctionFactory.GetSnakeCase(item.Name) == functionName);

    public void TestMethod(EnemyComponent enemy, EnemyVM  vm) {

    }
}
