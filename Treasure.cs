using Raylib_cs;

public class Treasure : GameObject
{
    private int _treasureVelocity;
    public Treasure(int x, int y, int width, int height, int fallWait) : base (x, y, width, height, fallWait)
    {
        _treasureVelocity = 7;
    }

    public override void ProcessActions()
    {
        base.ProcessActions();
    }
    public override void FallNow()
    {
        _y += _treasureVelocity;
    }
    public void PointsCollected(Player player)
    {
        player.CollectTreasure(10);
    }
    public override void Draw()
    {
        Raylib.DrawCircle(_x, _y, 10, Color.Gold);
    }
}