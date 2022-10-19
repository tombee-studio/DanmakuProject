using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D), typeof(Text))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter { get => _interpreter ??= new EnemyInterpreter(this); }
    Dictionary<int, List<BulletComponent>> bulletsList = new Dictionary<int, List<BulletComponent>>();
    [SerializeField] BulletComponent? bulletPrefab;

    [SerializeField] private Text script => GetComponent<Text>();

    void Start()
    {
        interpreter.test_run();
        LoadScript();
    }
    private List<ExpASTNodeBase> GetArgsList(params PrimitiveValue[] args)
    {
        return new List<ExpASTNodeBase>(
            args.Select(e => (ExpASTNodeBase)new PrimaryExpASTNode(e))
        );
    }
    [System.Obsolete("とりあえず用意しただけのメソッドに紐づいているメソッドなのでいずれ消します")]
    private List<T> getList<T>(params T[] elements)
    {
        return elements.ToList();
    }

    void LoadScript()
    {
        var tokens = interpreter.compiler.lexer.Lex(script.text);
        var ast = interpreter.compiler.parser.ParseBehaviour(
            new TokenStreamPointer(tokens));

        var enemy = GameObject.FindObjectOfType<EnemyComponent>();
        var vm = new EnemyVM(enemy);
        var vtable = new Dictionary<string, int>();
        var instructions = ast.ParsedNode
            .Compile(new Dictionary<string, int>())
            .ToList();
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
