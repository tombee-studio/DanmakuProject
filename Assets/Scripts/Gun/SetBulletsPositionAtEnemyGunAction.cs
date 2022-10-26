using System;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletsPositionAtEnemyGunAction : GunAction
{
    public override List<BulletComponent> Run(EnemyComponent enemy, List<BulletComponent> bullets)
    {
        bullets.ForEach(bullet => bullet.EnqueueAction(new BulletSetRelativePosition(
            bullet,
            Vector3.zero,
            enemy.transform)));
        return bullets;
    }
}
