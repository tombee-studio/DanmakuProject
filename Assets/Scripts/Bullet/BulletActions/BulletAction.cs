#nullable enable
public abstract class BulletAction
{
    protected BulletComponent bullet;
    protected int _frame = 0;
    public int frame { get => _frame; }
    public bool isEnd { get => IsEnd(); }
    public BulletAction(BulletComponent bullet)
    {
        this.bullet = bullet;
    }
    protected abstract bool IsEnd();
    public abstract void Run();
    public void incrementFrame()
    {
        this._frame++;
    }
}
