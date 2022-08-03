using System;
using UnityEngine;
public partial class EnemyVM
{
    public class EnemyVMException : Exception
    {
        public EnemyVMException(string message, EnemyVM vm) : base(message)
        {
            Debug.Log(vm.memory);
        }
    };
}
