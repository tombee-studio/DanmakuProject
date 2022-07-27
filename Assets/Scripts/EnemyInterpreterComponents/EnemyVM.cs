using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VMValueType = System.Int32;

public class EnemyVM
{
    public class EnemyVMException: Exception {
        public EnemyVMException(string message, EnemyVM vm) : base(message) {
            Debug.Log(vm.data.ToArray());
        }
    };

    private Stack<VMValueType> data = new Stack<VMValueType>();
    private bool isContinue = false;
    private bool isExit = true;  
    private int retVal = 0;

    public bool IsContinute { get => isContinue; }
    public bool IsExit { get => isExit; }
    public int ReturnValue {
        get {
            if (data.Count == 0) {
                throw new EnemyVMException(
                    $"Stack Size must be more than {data.Count}",
                    this);
            }
            return data.Pop();
        }
    }
    //TODO: functions (何型?)

    public void run(){

    }
}
 