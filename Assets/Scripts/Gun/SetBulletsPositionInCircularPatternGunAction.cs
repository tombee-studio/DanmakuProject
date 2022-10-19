using System;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletsPositionInCircularPatternGunAction: GunAction
{
    float angleOffset;

    public SetBulletsPositionInCircularPatternGunAction(float angleOffset): base()
    {
        this.angleOffset = angleOffset;
    }

    public override List<BulletComponent> Run(EnemyComponent enemy, List<BulletComponent> bullets)
    {
        int i = 0;
        bullets.ForEach(bullet =>
        {
            float deg = i * (360f / bullets.Count);
            Vector3 relativePos = new Vector3(Mathf.Cos(
                Mathf.Deg2Rad * (deg + angleOffset)),
                Mathf.Sin(Mathf.Deg2Rad * (deg + angleOffset)),
                0f);
            bullet.EnqueueAction(new BulletSetRelativePosition(bullet, relativePos, enemy.transform));
            i++;
        });
        return bullets;
    }
}
