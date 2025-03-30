using Raylib_cs;

public class Enemy : GameObject
{
    private int _enemyVelocity;

    public Enemy(int x, int y, int width, int height, int fallWait) : base (x, y, width, height, fallWait)
    {
        _enemyVelocity = 12;
    }

    public override void ProcessActions()
    {
        base.ProcessActions();
    }
    public override void FallNow()
    {
        _y += _enemyVelocity;
    }

    public void DamagePlayer(Player player)
    {
        player.PlayerHit(20);
    }

    public override void Draw()
    {
        Raylib.DrawCircle(_x, _y, 15, Color.Red);
    }
}