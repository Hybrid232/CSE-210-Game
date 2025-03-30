using Raylib_cs;

class GameManager
{
    public const int SCREEN_WIDTH = 1600;
    public const int SCREEN_HEIGHT = 950;

    private string _title;
    private List<GameObject> _gameObject = new List<GameObject>();
    private Player player;
    private int _enemySpawnTime = 0;
    private int _treasureSpawnTime = 0;
    private const int SPAWN_INTER = 120;
    private const int ENEMIES_SPAWN_RATE = 6;
    private const int TREASURE_SPAWN_RATE = 4;
    private Sound _treasureCollectedSND;
    private Sound _enemyExplosionSND;
    private Sound _gameOverSND;

    public GameManager()
    {
        _title = "CSE 210 Game";
        player = new Player(940, 850, 100, 10, 0);
        _gameObject.Add(player);
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            DrawElements();
            ProcessActions();

            Raylib.EndDrawing();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        _treasureCollectedSND = Raylib.LoadSound("C:/Users/emmit/OneDrive/Desktop/BYUI Work/Semester_3/CSE210-ProgrammingWithClasses/CSE-210-Game/coin-drop.mp3");
        _enemyExplosionSND = Raylib.LoadSound("C:/Users/emmit/OneDrive/Desktop/BYUI Work/Semester_3/CSE210-ProgrammingWithClasses/CSE-210-Game/bomb-enemy.mp3");
        _gameOverSND = Raylib.LoadSound("C:/Users/emmit/OneDrive/Desktop/BYUI Work/Semester_3/CSE210-ProgrammingWithClasses/CSE-210-Game/game-over.mp3");
    }

    private bool IsCollision(GameObject first, GameObject second)
    {
        int fLeft = first.GetLeftEdge();
        int fRight = first.GetRightEdge();
        int fTop = first.GetTopEdge();
        int fBottom = first.GetBottomEdge();

        int sLeft = second.GetLeftEdge();
        int sRight = second.GetRightEdge();
        int sTop = second.GetTopEdge();
        int sBottom = second.GetBottomEdge();

        bool collision = fRight > sLeft && fLeft < sRight && fBottom > sTop && fTop < sBottom;


        return collision;

    }

    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {
       foreach (GameObject create in _gameObject)
       {
            create.HandleInput();
       }
      
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    private void ProcessActions()
    {   
        _enemySpawnTime++;
        _treasureSpawnTime++;

        if (_enemySpawnTime >= SPAWN_INTER)
        {
            SpawnEnemy(ENEMIES_SPAWN_RATE);
            _enemySpawnTime = 0;
        }
        if (_treasureSpawnTime >= SPAWN_INTER)
        {
            SpawnTreasure(TREASURE_SPAWN_RATE);
            _treasureSpawnTime = 0;
        }

        List<GameObject> _objectDestroy = new List<GameObject>();
        foreach (GameObject create in _gameObject)
        {
            create.ProcessActions();
        }

        foreach (GameObject create in _gameObject)
        {
       

            if (create is Enemy enemy)
           {
            bool enemycollision = IsCollision(player, enemy);

            if (enemycollision)
            {
                Raylib.PlaySound(_enemyExplosionSND);
                player.PlayerHit(20);
                _objectDestroy.Add(enemy);
            }
           }
           if (create is Treasure treasure)
           {
            bool treasureCollision = IsCollision(player, treasure);

            if (treasureCollision)
            {
                Raylib.PlaySound(_treasureCollectedSND);
                player.CollectTreasure(10);
                _objectDestroy.Add(treasure);
            }
            
        
           }
        }
    
       foreach (var obj in _objectDestroy)
       {
            _gameObject.Remove(obj);
       }


      if (player.GetHealth() == 0)
      {
        _gameObject.Remove(player);

        Raylib.DrawText($"GAME OVER", 350, 400, 150, Color.White);
        Raylib.PlaySound(_gameOverSND);
      }  
            
    }

    private void SpawnTreasure(int numOfTreasure)
    {
        for (int i = 0; i < numOfTreasure; i++)
        {
            int randomX = new Random().Next(0, SCREEN_WIDTH);
            int randomFall = new Random().Next(40, 110);
            GameObject treasure = new Treasure(randomX, -20, 8, 10, randomFall);
            _gameObject.Add(treasure);

        }
      
    }

    private void SpawnEnemy(int numOfEnemy)
    {
        for (int i = 0; i < numOfEnemy; i++)
        {
            int randomX = new Random().Next(0, SCREEN_WIDTH);
            int randomFall = new Random().Next(20, 80);
            GameObject enemy = new Enemy(randomX, -20, 10, 12, randomFall);
            _gameObject.Add(enemy);
        }
    }

    /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
       foreach (GameObject create in _gameObject)
       {
            create.Draw();
       }
       Raylib.DrawText($"Health: {player.GetHealth()}", 10, 10, 20, Color.Red);    
       Raylib.DrawText($"Treasure Collected: {player.GetPoints()}", 10, 40, 20, Color.Gold); 
    
        

    }
}