using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter{ get => _interpreter ??= new EnemyInterpreter(this); }
    List<BulletComponent> bullets = new List<BulletComponent>(); 
    //TODO: source を追加 (何型?)
    [SerializeField] BulletComponent? bulletPrefab;
    void Start()
    {
        interpreter.test_run();
        Debug.Log(interpreter.ReturnValue);
        GenerateBullets(24); // とりあえず
    }

    void Update()
    {
        Move(transform.position.x, transform.position.y);  // とりあえずの動き
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }

    // 上下左右移動
    public void Move(float x, float y)
    {
        Vector3 newPosition = transform.position + new Vector3(x, y, 0);

        // とりあえず, 画面端に到達したらそれ以上移動しない
        Vector3 max = WindowInformation.UP_RIGHT;
        Vector3 min = WindowInformation.DOWN_LEFT;
        if (newPosition.x > max.x) { newPosition.x = max.x; }
        if (newPosition.y > max.y) { newPosition.y = max.y; }
        if (newPosition.x < min.x) { newPosition.x = min.x; }
        if (newPosition.y < min.x) { newPosition.y = min.y; }
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
    // 弾生成 (とりあえず線形移動)
    public void GenerateBullets(int bulletCount)
    {
        if (bulletPrefab == null) { throw new System.NullReferenceException("Set bullet Prefab from inspector."); }
        for (int i = 0; i < bulletCount; i++)
        {
            BulletComponent bullet = Instantiate(bulletPrefab, transform);
            float deg = i * (360f / bulletCount);
            bullet.EnqueueAction(new BulletMoveLinear(bullet.transform, 0.1f, deg)); // とりあえず
            bullets.Add(bullet);
        }
    }
}
