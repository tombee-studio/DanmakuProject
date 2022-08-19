using UnityEngine;

#nullable enable
public class BulletSetAbsolutePosition : BulletAction
{
    bool _isEnd = false;
    Vector3 registeredPosition;
    public BulletSetAbsolutePosition(BulletComponent bullet, Vector3 position, Transform enemyTransform = null) : base(bullet)
    {
        this.registeredPosition = position;
    }

    public override void Run()
    {
        bullet.transform.position = registeredPosition;
        _isEnd = true;
    }
    protected override bool IsEnd() { return _isEnd; }
}
