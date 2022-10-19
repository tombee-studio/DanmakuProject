using System;
using System.Reflection;
using System.Linq;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class EnemyFunctionFactory
{
    private MethodInfo[] functions;

    private EnemyFunctionFactory() {
        BindingFlags flag =
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.FlattenHierarchy |
            BindingFlags.NonPublic;
        functions = GetType().GetMethods(flag);
    }

    private static EnemyFunctionFactory _singleton;
    private static EnemyFunctionFactory singleton {
        get => _singleton ??= new EnemyFunctionFactory();
    }

    private static string GetUpperCamelCase(string str)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        return String.Join("", str.Split("_").Select(s => textInfo.ToTitleCase(s)));
    }

    public static EnemyFunctionFactory GetInstance() =>
        _singleton ??= new EnemyFunctionFactory();

    public void Call(
        int functionCode,
        EnemyComponent enemyComponent,
        EnemyVM enemyVM
    ){
        object[] args = new object[] {
            enemyComponent,
            enemyVM
        };
        functions[functionCode].Invoke(this, args);
    }

    public int Find(String functionName)
    {
        return Array.FindIndex(functions,
            item => EnemyFunctionFactory.GetUpperCamelCase(functionName) == item.Name);
    }

    public void TestMethod() {

    }

    public void GenerateBullets(EnemyComponent enemy, EnemyVM vm){
        int id = vm.PopFromStack();
        int bulletsCount = vm.PopFromStack();
        enemy.GenerateBullets(id, bulletsCount);
    }

    public void ActivateBullets(EnemyComponent enemy, EnemyVM vm){
        int id = vm.PopFromStack();
        enemy.ActivateBullets(id);
    }

    public void DelayBullets(EnemyComponent enemy, EnemyVM vm) {
        int id = vm.PopFromStack();
        int frames = vm.PopFromStack();
        enemy.DelayBullets(id, frames);
    }

    public void SetBulletsPositionAtEnemy(EnemyComponent enemy, EnemyVM vm)
    {
        int id = vm.PopFromStack();
        enemy.SetBulletsPositionAtEnemy(id);
    }

    public void MoveBulletsParallel(EnemyComponent enemy, EnemyVM vm)
    {
        int id = vm.PopFromStack();
        float angleOffset = vm.PopFromStack();
        float speed = vm.PopFromStack();
        enemy.MoveBulletsParallel(id, speed, angleOffset);
    }
    
    public void SetBulletsPositionInCircularPattern(EnemyComponent enemy, EnemyVM vm)
    {
        int id = vm.PopFromStack();
        float angleOffset = vm.PopFromStack();
        enemy.SetBulletsPositionInCircularPattern(id, angleOffset);
    }

    public void ScatterBulletsInCircularPattern(EnemyComponent enemy, EnemyVM vm)
    {
        int id = vm.PopFromStack();
        float angleOffset = vm.PopFromStack();
        float speed = vm.PopFromStack();
        enemy.ScatterBulletsInCircularPattern(id, speed, angleOffset);
    }

    public void Wait(EnemyComponent enemy, EnemyVM vm)
    {
        int frames = vm.PopFromStack();
        enemy.SetDelayTime(frames);
    }
}
