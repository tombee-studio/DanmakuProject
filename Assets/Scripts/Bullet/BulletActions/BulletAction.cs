using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletAction
{
    protected int frame = 0;

    public abstract bool IsEnd();
    public abstract void Run();
}
