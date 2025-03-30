public abstract class GameObject 
{
    protected int _x;
    protected int _y;
    protected int _width;
    protected int _height;
    protected int _fallWait;
    protected int _timeSpawn;

    public GameObject(int x, int y, int width, int height, int fallWait)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _fallWait = fallWait;
        _timeSpawn = 0;
    }
    public abstract void Draw();
    
    public virtual int GetLeftEdge()
    {
        return _x;
    }
    public virtual int GetRightEdge()
    {
        return _x + _width;
    }
    public virtual int GetTopEdge()
    {
        return _y;
    }
    public virtual int GetBottomEdge()
    {
        return _y + _height;
    }
    public virtual void HandleInput()
    {
        _timeSpawn ++;

        if(_timeSpawn >= _fallWait)
        {
            FallNow();
        }
    }
    public virtual void ProcessActions()
    {

    }
    public virtual void FallNow()
    {
        
    }
   
}