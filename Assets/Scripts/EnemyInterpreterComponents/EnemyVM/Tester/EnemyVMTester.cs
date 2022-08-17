using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine.Assertions;
using UnityEngine;
using System.Collections.Generic;

public partial class EnemyVMTester
{
    
    public EnemyVM vm;
    public bool IsContinue { get => vm.IsContinute; }
    public bool IsExit { get => vm.IsExit; }
    public int ReturnValue { get => vm.ReturnValue; }

    public EnemyVMTester()
    {
        var component = new EnemyComponent();
        vm = new EnemyVM(component);
    } 

    private void run()
    {
        vm.run();
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
            var target = new EnemyVMTester();
            object[] parametersArray = new object[] { };
            method.Invoke(target, parametersArray);
        }
        Debug.Log($"✅ Check All {testFunctions.Length} cases.");
    }
    
}
