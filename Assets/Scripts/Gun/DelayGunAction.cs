using System;
using System.Collections.Generic;
using System.Linq;

public class DelayGunAction : GunAction
{
    int frames;

    public DelayGunAction(int frames) : base() {
        this.frames = frames;
    }

    public override List<BulletComponent> Run(
        EnemyComponent enemy,
        List<BulletComponent> bullets)
    {
        bullets.ForEach(bullet =>
            bullet.EnqueueAction(new BulletDelay(bullet, frames)));
        return bullets;
    }
}
