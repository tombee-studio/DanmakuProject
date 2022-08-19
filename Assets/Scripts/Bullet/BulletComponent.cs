using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class BulletComponent : MonoBehaviour
{
    Queue<BulletAction> bulletActions = new Queue<BulletAction>();
    BulletAction bulletAction { get => bulletActions.Peek(); }
    public Vector3 velocity;
    bool isActive = false;

    private void FixedUpdate()
    {
        if (!this.isActive) { return; }
        transform.position += velocity;
        bulletAction.incrementFrame();
    }
    void Update()
    {
        if (!this.isActive) { return; }
        if (bulletAction.isEnd)
        {
            bulletActions.Dequeue();
            if (bulletActions.Count == 0)
            {
                End();
                return;
            }
        }
        bulletAction.Run();
    }

    public void EnqueueAction(BulletAction bulletAction)
    {
        bulletActions.Enqueue(bulletAction);
    }
    public void Activate()
    {
        this.isActive = true;
    }
    public void Pause()
    {
        this.isActive = false;
    }
    public void End()
    {
        Destroy(gameObject);
    }
}
