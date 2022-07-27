using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyComponent : MonoBehaviour
{
    #nullable enable
    EnemyInterpreter? _interpreter = null;

    EnemyInterpreter interpreter{ get => _interpreter ??= new EnemyInterpreter(); }
    List<GameObject> bullets = new List<GameObject>(); 
    //TODO: source を追加 (何型?)

    void Start()
    { 
    }

    void Update()
    {
        // TODO: 上下左右移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);  
    }
}
 