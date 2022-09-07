using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class Tester
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
    public void runTests()
    {
        var testFunctions = pickupTestMethods();
        foreach (MethodInfo method in testFunctions)
        {
            var target = new Tester();
            object[] parametersArray = new object[] { };
            method.Invoke(target, parametersArray);
        }
        Debug.Log($"✅ Check All {testFunctions.Length} cases.");
    }
}
