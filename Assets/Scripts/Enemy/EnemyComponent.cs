using System;
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
    Dictionary<int, List<GunAction>> gunActions = new Dictionary<int, List<GunAction>>();

    [SerializeField] BulletComponent? bulletPrefab;

    [SerializeField] private Text script => GetComponent<Text>();

    private int delayTime = 0;

    private EnemyVM vm;

    void Start()
    {
        vm = new EnemyVM(this);
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
        vm = new EnemyVM(enemy);
        var vtable = new Dictionary<string, int>();
        var instructions = ast.ParsedNode
            .Compile(new Dictionary<string, int>())
            .ToList();
        instructions.ForEach(instruction => vm.appendInstruction(instruction));
    }

    void Update()
    {
        if (delayTime > 0)
        {
            delayTime--;
        }
        else {
            while (!vm.IsExit && delayTime < 1)
            {
                vm.run();
            }
        }
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

        gunActions.Add(id, new List<GunAction>());
        gunActions[id].Add(new GenerateBulletGunAction(bulletCount));
    }
    public void ActivateBullets(int id)
    {
        var bullets = new List<BulletComponent>();
        foreach (var gunAction in gunActions[id]) {
            bullets = gunAction.Run(this, bullets);
        }
        foreach (var bullet in bullets) {
            bullet.Activate();
        }
    }
    public void DelayBullets(int id, int frames)
    {
        gunActions[id].Add(new DelayGunAction(frames));
    }
    public void SetBulletsPositionAtEnemy(int id)
    {
        gunActions[id].Add(new SetBulletsPositionAtEnemyGunAction());
    }
    public void MoveBulletsParallel(int id, float speed, float angleOffset)
    {
        gunActions[id].Add(
            new MoveBulletsParallelGunAction(speed, angleOffset));
    }
    public void SetBulletsPositionInCircularPattern(int id, float angleOffset/* = 0*/)
    {
        gunActions[id].Add(
            new SetBulletsPositionInCircularPatternGunAction(angleOffset));
    }
    public void ScatterBulletsInCircularPattern(int id, float speed, float angleOffset/* = 0*/)
    {
        gunActions[id].Add(
            new ScatterBulletsInCircularPatternGunAction(speed, angleOffset));
    }

    public void SetDelayTime(int delayTime) {
        this.delayTime = delayTime;
    }

    public BulletComponent GenerateBullets()
    {
        return Instantiate(bulletPrefab);
    }
}
