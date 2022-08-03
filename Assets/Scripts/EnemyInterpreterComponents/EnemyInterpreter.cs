using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;


public class EnemyInterpreter
{
    #nullable enable
    EnemyCompiler? _compiler=null;
    public EnemyCompiler compiler {get => _compiler ??= new EnemyCompiler();}

    EnemyVM? _vm;
    public EnemyVM vm {get => _vm ??= new EnemyVM(); }
    public bool IsContinue { get =>  vm.IsContinute; }
    public bool IsExit {  get => vm.IsExit; }
    public int ReturnValue { get => vm.ReturnValue; }

    public void run(){
        vm.run();
    }

    public void test_run() {
        var tmp_interpreter = new EnemyInterpreter();
        BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        flag |= BindingFlags.NonPublic;
        MethodInfo[] methods = tmp_interpreter.GetType().GetMethods(flag);
        foreach (MethodInfo method in methods)
        {
            if (!Regex.IsMatch(method.Name, "test_run_")) continue;
            
            object[] parametersArray = new object[] {};
            method.Invoke(tmp_interpreter, parametersArray);
        }
    }
}
