using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public void test_run()
    {
        //TODO: 命令を追加
        while (IsContinue) run();
        Debug.Assert(false);
    }
}
