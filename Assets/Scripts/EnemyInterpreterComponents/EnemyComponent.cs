using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter { get => _interpreter ??= new EnemyInterpreter(); }
    List<BulletComponent> bullets = new List<BulletComponent>();
    //TODO: source を追加 (何型?)
    [SerializeField] BulletComponent? bulletPrefab;
    void Start()
    {
        interpreter.test_run();
        Debug.Log(interpreter.ReturnValue);
        GenerateBullets(); // とりあえず
    }

    void Update()
    {
        MoveRight();  // とりあえずの動き
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }

    // とりあえずの動き
    private void MoveRight()
    {
        // BEGIN: x+1
        EnemyInterpreter interpreter = new EnemyInterpreter();
        interpreter.vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, (int)transform.position.x)
        );
        interpreter.vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 1)
        );
        interpreter.vm.appendInstruction(
            new EnemyVM.Instruction(EnemyVM.Mnemonic.ADD, 0)
        );
        while (!interpreter.IsExit) interpreter.run();
        // END: x+1
        float x = interpreter.vm.ReturnValue;

        // とりあえず, 画面端に到達したら x = 0 に戻る
        float max_x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        if (x > max_x) { x = 0; }
        transform.position = new Vector3(
                x,
                transform.position.y,
                transform.position.z
            );
    }
    // とりあえずの動き
    public void GenerateBullets()
    {
        if (bulletPrefab == null) { throw new System.NullReferenceException("Set bullet Prefab from inspector."); }
        // とりあえず 1 つ生成
        for (int i = 0; i < 12; i++)
        {
            BulletComponent bullet = Instantiate(bulletPrefab, transform);
            // BEGIN: i*30
            EnemyInterpreter interpreter = new EnemyInterpreter();
            interpreter.vm.appendInstruction(
                new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, i)
            );
            interpreter.vm.appendInstruction(
                new EnemyVM.Instruction(EnemyVM.Mnemonic.PUSH, 30)
            );
            interpreter.vm.appendInstruction(
                new EnemyVM.Instruction(EnemyVM.Mnemonic.MUL, 0)
            );
            while (!interpreter.IsExit) interpreter.run();
            // END: i*30
            int deg = interpreter.vm.ReturnValue;
            bullet.EnqueueAction(new BulletMoveLinear(bullet.transform, 0.1f, deg));
            bullets.Add(bullet);
        }
    }
}
