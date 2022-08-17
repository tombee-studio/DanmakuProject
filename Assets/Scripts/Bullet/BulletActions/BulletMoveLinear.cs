using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLinear : BulletAction
{
    Vector3 velocity;
    Transform transform;
    public BulletMoveLinear(Transform transform, float speed, float angleDegree)
    {
        this.transform = transform;
        float angle = Mathf.Deg2Rad * angleDegree;
        velocity = speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }

    // TODO: イベントループを利用した形式に直す
    // イメージ: この関数が一度呼ばれた後は BulletDelay.Run() の方で待ちが発生する
    public override void Run()
    {
        transform.position += velocity;
    }
    public override bool IsEnd()
    {
        // 画面端に行ったら終わり
        Vector3 max = WindowInformation.DOWN_LEFT;
        Vector3 min = WindowInformation.UP_RIGHT;
        if (
            false
            || transform.position.x < max.x
            || transform.position.y < max.y
            || transform.position.x > min.x
            || transform.position.y > min.y
        ) { return true; }

        // デフォルトは false
        return false;
    }
}
