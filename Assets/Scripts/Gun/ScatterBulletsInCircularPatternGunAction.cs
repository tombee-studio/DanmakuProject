using System;
using System.Collections.Generic;

public class ScatterBulletsInCircularPatternGunAction: GunAction
{
    float speed;
    float angleOffset;
    public ScatterBulletsInCircularPatternGunAction(float speed, float angleOffset)
    {
        this.speed = speed;
        this.angleOffset = angleOffset;
    }

    public override List<BulletComponent> Run(EnemyComponent enemy, List<BulletComponent> bullets)
    {
        int i = 0;
        bullets.ForEach(bullet =>
        {
            float deg = i * (360f / bullets.Count);
            bullet.EnqueueAction(new BulletMoveLinear(bullet, speed, deg + angleOffset));
            i++;
        });
        return bullets;
    }
}
