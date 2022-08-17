using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDelay : BulletAction
{
    int waitFrames;
    BulletDelay(int waitFrames)
    {
        this.waitFrames = waitFrames;
    }
    public override bool IsEnd()
    {
        if (this.frame >= waitFrames) { return true; }
        return false;
    }
    public override void Run()
    {
        // DoNothing();
    }
}
