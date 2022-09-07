using UnityEngine;

#nullable enable
public class BulletSetRelativePosition : BulletAction
{
    bool _isEnd = false;
    Vector3 registeredPosition;
    Transform enemyTransform;
    public BulletSetRelativePosition(BulletComponent bullet, Vector3 relativePosition, Transform enemyTransform) : base(bullet)
    {
        this.enemyTransform = enemyTransform;
        this.registeredPosition = relativePosition;
    }

    public override void Run()
    {
        bullet.transform.position = enemyTransform.position + registeredPosition;
        _isEnd = true;
    }
    protected override bool IsEnd() { return _isEnd; }
}
