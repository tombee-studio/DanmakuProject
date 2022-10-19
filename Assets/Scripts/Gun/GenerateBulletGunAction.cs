using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBulletGunAction : GunAction
{
    int bulletCount;

    public GenerateBulletGunAction(int bulletCount) : base() {
        this.bulletCount = bulletCount;
    }

    public override List<BulletComponent> Run(EnemyComponent enemy, List<BulletComponent> bullets)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            bullets.Add(enemy.GenerateBullets().GetComponent<BulletComponent>());
        }
        return bullets;
    }
}
