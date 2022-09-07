using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyFunctionsFatoryTester
{
    public void test_enemyfunctionfactory001() {
        int index = EnemyFunctionFactory.GetInstance().Find("test_method");
        Assert.AreNotEqual(index, -1);
    }

    public void test_enemyfunctionfactory002()
    {
        int index = EnemyFunctionFactory.GetInstance().Find("delay_bullets");
        Assert.AreNotEqual(index, -1);
    }

    public void test_enemyfunctionfactory003()
    {
        int index = EnemyFunctionFactory.GetInstance().Find("set_bullets_position_at_enemy");
        Assert.AreNotEqual(index, -1);
    }

    public void test_enemyfunctionfactory004()
    {
        int index = EnemyFunctionFactory.GetInstance().Find("move_bullets_parallel");
        Assert.AreNotEqual(index, -1);
    }

    public void test_enemyfunctionfactory005()
    {
        int index = EnemyFunctionFactory.GetInstance().Find("set_bullets_position_in_circular_pattern");
        Assert.AreNotEqual(index, -1);
    }

    public void test_enemyfunctionfactory006()
    {
        int index = EnemyFunctionFactory.GetInstance().Find("scatter_bullets_in_circular_pattern");
        Assert.AreNotEqual(index, -1);
    }

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

    public void runTests()
    {
        var testFunctions = pickupTestMethods();
        foreach (MethodInfo method in testFunctions)
        {
            var target = new EnemyFunctionsFatoryTester ();
            object[] parametersArray = new object[] { };
            method.Invoke(target, parametersArray);
        }
        Debug.Log($"✅ Check All {testFunctions.Length} cases.");
    }
}
