using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter { get => _interpreter ??= new EnemyInterpreter(this); }
    Dictionary<int, List<BulletComponent>> bulletsList = new Dictionary<int, List<BulletComponent>>();
    [SerializeField] BulletComponent? bulletPrefab;
    void Start()
    {
        interpreter.test_run();
        Debug.Log(interpreter.ReturnValue);
#pragma warning disable CS0618  // ここではわざと使っているので警告を出さない.
        Toriaezu();
#pragma warning restore CS0618
    }
    private List<ExpASTNode> GetArgsList(params PrimitiveValue[] args)
    {
        return new List<ExpASTNode>(
            args.Select(e => (ExpASTNode)new PrimaryExpASTNode(e))
        );
    }
    [System.Obsolete("とりあえず用意しただけのメソッドに紐づいているメソッドなのでいずれ消します")]
    private List<T> getList<T>(params T[] elements)
    {
        return elements.ToList();
    }
    // とりあえず動作させる
    [System.Obsolete("とりあえず用意しただけのメソッドなのでいずれ消します")]
    void Toriaezu()
    {
        /* サンプルプログラム */
        /*
            bullet >>
            ID: 0;
            generate_bullets(24);
            set_bullets_position_at_enemy(id);
            scatter_bullets_in_circular_pattern(0.1f, 0f);
            delay_bullets(60);
            scatter_bullets_in_circular_pattern(0.1f, 180f);
            delay_bullets(120);

            ID: 1;
            generate_bullets(24);
            delay_bullets(60);
            set_bullets_position_at_enemy(id);
            scatter_bullets_in_circular_pattern(0.1f, 0f);
            delay_bullets(60);
            move_bullets_parallel(0.1f, 30f);
            delay_bullets(120);

            action >>
            activate_bullets(0);
            activate_bullets(1);
        */
        /* 上のサンプルに対応する AST の動作を確認する */
        int id;
        id = 0;
        var nodes = getList(
            new CallFuncStASTNode(
                id,
                "generate_bullets",
                GetArgsList(24)
            ),
            new CallFuncStASTNode(
                id,
                "set_bullets_position_at_enemy",
                GetArgsList()
            ),
            new CallFuncStASTNode(
                id,
                "scatter_bullets_in_circular_pattern",
                GetArgsList(0.1f, 0f)
            ),
            new CallFuncStASTNode(
                id,
                "delay_bullets",
                GetArgsList(60)
            ),
            new CallFuncStASTNode(
                id,
                "scatter_bullets_in_circular_pattern",
                GetArgsList(0.1f, 180f)
            ),
            new CallFuncStASTNode(
                id,
                "delay_bullets",
                GetArgsList(120)
            ),
            new CallFuncStASTNode(
                id,
                "activate_bullets",
                GetArgsList()
            )
        );

        id = 1;
        nodes.AddRange(
            getList(
                new CallFuncStASTNode(
                    id,
                    "generate_bullets",
                    GetArgsList(24)
                ),
                new CallFuncStASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(60)
                ),
                new CallFuncStASTNode(
                    id,
                    "set_bullets_position_at_enemy",
                    GetArgsList()
                ),
                new CallFuncStASTNode(
                    id,
                    "scatter_bullets_in_circular_pattern",
                    GetArgsList(0.1f, 0f)
                ),
                new CallFuncStASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(60)
                ),
                new CallFuncStASTNode(
                    id,
                    "move_bullets_parallel",
                    GetArgsList(0.1f, 30f)
                ),
                new CallFuncStASTNode(
                    id,
                    "delay_bullets",
                    GetArgsList(120)
                ),
                new CallFuncStASTNode(
                    id,
                    "activate_bullets",
                    GetArgsList()
                )
            )
        );

        var enemy = GameObject.FindObjectOfType<EnemyComponent>();
        var vm = new EnemyVM(enemy);
        var vtable = new Dictionary<string, int>();
        var instructions = nodes.Select(node => node.Compile(vtable))
            .SelectMany(instructions => instructions).ToList();
        instructions.ForEach(instruction => vm.appendInstruction(instruction));
        while (!vm.IsExit)
        {
            vm.run();
        }
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
