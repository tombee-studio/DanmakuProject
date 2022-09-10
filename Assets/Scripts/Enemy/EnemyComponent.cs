using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter { get => _interpreter ??= new EnemyInterpreter(this); }
    Dictionary<int, List<BulletComponent>> bulletsList = new Dictionary<int, List<BulletComponent>>();
    //TODO: source を追加 (何型?)
    [SerializeField] BulletComponent? bulletPrefab;
    void Start()
    {
        interpreter.test_run();
        Debug.Log(interpreter.ReturnValue);
#pragma warning disable CS0618  // ここではわざと使っているので警告を出さない.
        Toriaezu();
#pragma warning restore CS0618
    }
    // とりあえず動作させる
    [System.Obsolete("とりあえず用意しただけのメソッドなのでいずれ消します")]
    void Toriaezu()
    {
        /*
        int id;
        id = 0;
        GenerateBullets(id, 24);
        SetBulletsPositionAtEnemy(id);
        ScatterBulletsInCircularPattern(id, 0.1f, 0f);
        DelayBullets(id, 60);
        ScatterBulletsInCircularPattern(id, 0.1f, 180f);
        DelayBullets(id, 120);

        id = 1;
        GenerateBullets(id, 24);
        DelayBullets(id, 60);
        SetBulletsPositionAtEnemy(id);
        ScatterBulletsInCircularPattern(id, 0.1f, 0f);
        DelayBullets(id, 60);
        MoveBulletsParallel(id, 0.1f, 30f);
        DelayBullets(id, 120);

        ActivateBullets(0);
        ActivateBullets(1);
        //*/
    }

    void Update()
    {
        Move(0.25f * Mathf.Cos(Mathf.Deg2Rad * 30), 0.25f * Mathf.Sin(Mathf.Deg2Rad * 30));  // とりあえずの動き
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
    public void GenerateBullets(int id, int bulletCount)
    {
        if (bulletPrefab == null) { throw new System.NullReferenceException("Set bullet Prefab from inspector."); }
        if (bulletsList.ContainsKey(id)) { throw new System.Exception($"Duplicated Key Exception: id = {id} already used. (id = {id} は既に使われています.)"); }

        bulletsList.Add(id, new List<BulletComponent>());
        for (int i = 0; i < bulletCount; i++)
        {
            bulletsList[id].Add(Instantiate(bulletPrefab));
        }
        // 画面外で初期化. 弾は活動開始まで画面外で待機.
        Vector3 extreme = 100 * WindowInformation.UP_RIGHT;
        bulletsList[id].ForEach(bullet => bullet.transform.position = new Vector3(extreme.x, extreme.y, 0));
    }
    public void ActivateBullets(int id)
    {
        bulletsList[id].ForEach(bullet => bullet.Activate());
    }
    public void DelayBullets(int id, int frames)
    {
        bulletsList[id].ForEach(bullet => bullet.EnqueueAction(new BulletDelay(bullet, frames)));
    }
    public void SetBulletsPositionAtEnemy(int id)
    {
        foreach(var k in bulletsList.Keys){
            Debug.Log(k);
        }
        bulletsList[id].ForEach(bullet => bullet.EnqueueAction(new BulletSetRelativePosition(bullet, Vector3.zero, transform)));
    }
    public void MoveBulletsParallel(int id, float speed, float angleOffset)
    {
        bulletsList[id].ForEach(bullet => bullet.EnqueueAction(new BulletMoveLinear(bullet, speed, angleOffset)));
    }
    public void SetBulletsPositionInCircularPattern(int id, float angleOffset/* = 0*/)
    {
        int i = 0;
        bulletsList[id].ForEach(bullet =>
        {
            float deg = i * (360f / bulletsList[id].Count);
            Vector3 relativePos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (deg + angleOffset)), Mathf.Sin(Mathf.Deg2Rad * (deg + angleOffset)), 0f);
            bullet.EnqueueAction(new BulletSetRelativePosition(bullet, relativePos, transform));
            i++;
        });
    }
    public void ScatterBulletsInCircularPattern(int id, float speed, float angleOffset/* = 0*/)
    {
        int i = 0;
        bulletsList[id].ForEach(bullet =>
        {
            float deg = i * (360f / bulletsList[id].Count);
            bullet.EnqueueAction(new BulletMoveLinear(bullet, speed, deg + angleOffset));
            i++;
        });
    }
}
