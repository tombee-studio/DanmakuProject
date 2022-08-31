using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

/**
 * このテスターを継承するとテスト機能をすぐ使用できる。
 * 継承先のクラスでテスト対象の変数を用意し、test_から始まる関数を定義すると良い。
 */
public abstract class Tester
{
    private MethodInfo[] selectTestMethods(MethodInfo[] methods)
    {
        var testMethods = new List<MethodInfo>();
        foreach (var method in methods)
        {
            if (!Regex.IsMatch(method.Name, "test_")) continue;
            testMethods.Add(method);
        }

        return testMethods.ToArray();

    }
    private MethodInfo[] pickupTestMethods()
    {
        BindingFlags flag =
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.FlattenHierarchy |
            BindingFlags.NonPublic;
        MethodInfo[] methods = GetType().GetMethods(flag);
        return selectTestMethods(methods);
    }
    protected abstract Tester cloneThisObject();
    public void runTests()
    {
        var testFunctions = pickupTestMethods();
        foreach (MethodInfo method in testFunctions)
        {
            var tester = cloneThisObject();
            object[] parametersArray = new object[] { };
            method.Invoke(tester, parametersArray);
        }
        Debug.Log($"✅ Check All {testFunctions.Length} cases.");
    }
}
