using System;
using System.Collections.Generic;

public abstract class GunAction
{
    public abstract List<BulletComponent> Run(
        EnemyComponent enemy,
        List<BulletComponent> bullets);
}
