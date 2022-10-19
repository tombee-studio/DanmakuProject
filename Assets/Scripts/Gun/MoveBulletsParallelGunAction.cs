using System;
using System.Collections.Generic;

public class MoveBulletsParallelGunAction: GunAction
{
    float speed;
    float angleOffset;

    public MoveBulletsParallelGunAction(float speed, float angleOffset)
    {
        this.speed = speed;
        this.angleOffset = angleOffset;
    }

    public override List<BulletComponent> Run(
        EnemyComponent enemy,
        List<BulletComponent> bullets)
    {
        bullets.ForEach(bullet => bullet.EnqueueAction(
            new BulletMoveLinear(bullet, speed, angleOffset)));
        return bullets;
    }
}
