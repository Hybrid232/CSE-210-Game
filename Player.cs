using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public class Player : GameObject
{
    private int _playerSpeed;
    private int _playerPoints;
    private int _playerHealth;
 
    public Player(int x, int y, int width, int height, int fallWait) : base(x, y, width, height, fallWait)
    {
        _playerSpeed = 20;
        _playerPoints = 0;
        _playerHealth = 100;
        
    }
    public int GetPoints()
    {
        return _playerPoints;
    }
    public int GetHealth()
    {
        return _playerHealth;
    }
    public void CollectTreasure(int points)
    {
        _playerPoints += points;
    }
    public void PlayerHit(int damage)
    {
        _playerHealth -= damage;
        if (_playerHealth < 0)
        {
            _playerHealth = 0;
        }
    }
   
    public override void HandleInput()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            _x = _x - _playerSpeed;  
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            _x = _x + _playerSpeed;    
        }

        if(GetLeftEdge() < 0)
        {
            _x = 0;
        }
        else if(_x + _width > GameManager.SCREEN_WIDTH)
        {
            _x = GameManager.SCREEN_WIDTH - _width;
        }
    }
    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, Color.SkyBlue);
    }

    public override void ProcessActions()
    {
        
    }
}