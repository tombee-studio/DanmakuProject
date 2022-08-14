using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
#nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter { get => _interpreter ??= new EnemyInterpreter(); }
    List<GameObject> bullets = new List<GameObject>();
    //TODO: source を追加 (何型?)

    void Start()
    {
        interpreter.test_run();
        Debug.Log(interpreter.ReturnValue);
    }

    void Update()
    {
        EnemyInterpreter interpreter = new EnemyInterpreter();
        // とりあえずの動き
        // BEGIN: 右に移動
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
        float x = interpreter.vm.ReturnValue;
        // 画面端に到達したら x = 0 に戻る
        float max_x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        if (x > max_x) { x = 0; }
        transform.position = new Vector3(
                x,
                transform.position.y,
                transform.position.z
            );
        // END: 右に移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
