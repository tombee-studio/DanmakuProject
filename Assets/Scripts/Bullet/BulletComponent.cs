using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    Queue<BulletAction> bulletActions = new Queue<BulletAction>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            var bulletAction = bulletActions.Peek();
            if (bulletAction.IsEnd())
            {
                BulletAction action = // とりあえず
                    bulletActions.Dequeue(); // これは残す.
                transform.position = new Vector3(0, 0, 0); // とりあえず
                EnqueueAction(action); // とりあえず
            }
            else { bulletAction.Run(); }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void EnqueueAction(BulletAction bulletAction)
    {
        bulletActions.Enqueue(bulletAction);
    }
}
