using UnityEngine;

#nullable enable
public class BulletMoveLinear : BulletAction
{
    Vector3 velocity;
    bool _isEnd = false;
    public BulletMoveLinear(BulletComponent bullet, float speed, float angleDegree) : base(bullet)
    {
        float angle = Mathf.Deg2Rad * angleDegree;
        this.velocity = speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }

    public override void Run()
    {
        bullet.velocity = velocity;
        _isEnd = true;
    }
    protected override bool IsEnd() { return _isEnd; }
}
