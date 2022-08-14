using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLinear : BulletAction
{
    int frame = 0;
    bool is_end = false;
    Vector3 velocity;
    Transform transform;
    public BulletMoveLinear(Transform transform, float speed, float angle_degree)
    {
        this.transform = transform;
        float angle = Mathf.Deg2Rad * angle_degree;
        velocity = speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }
    public override void run(){
        transform.position += velocity;
        float max_x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        if (transform.position.x > max_x) { is_end = true; }
    }
    public override bool isEnd()
    {
        return is_end;
    }
}
