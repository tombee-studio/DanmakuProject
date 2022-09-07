#nullable enable
public class BulletDelay : BulletAction
{
    int waitFrames;
    public BulletDelay(BulletComponent bullet, int waitFrames) : base(bullet)
    {
        this.waitFrames = waitFrames;
    }
    protected override bool IsEnd() { return this._frame >= waitFrames; }
    public override void Run()
    {
        // DoNothing();
    }
}
