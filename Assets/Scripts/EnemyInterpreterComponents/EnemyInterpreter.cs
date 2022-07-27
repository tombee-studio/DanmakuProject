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

    void run(){

    }

    void Update() {
        while(vm.IsContinute){
            vm.run();
        }

        if (vm.IsExit) {
            Debug.Log(vm.ReturnValue);
        }
    }
}
