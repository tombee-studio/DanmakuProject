using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    Queue<BulletAction> bulletActions = new Queue<BulletAction>();

    // Start is called before the first frame update
    void Start()
    {
        bulletActions.Enqueue(new BulletMoveLinear(transform, 0.1f, 30));
    }

    // Update is called once per frame
    void Update()
    {
        try{
            var bulletAction = bulletActions.Peek();
            if (bulletAction.isEnd()) { 
                transform.position = new Vector3(0, 0, 0); // とりあえず
                bulletActions.Dequeue();
                bulletActions.Enqueue(new BulletMoveLinear(transform, 0.1f, 30)); // とりあえず
            }
            else { bulletAction.run(); }
        } catch (System.Exception e){
            Debug.LogError(e);
        }
    }
}
