using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

public class ObjectCollision : BaseLayer
{
    public List<Object> Objects { get; set; } = new List<Object>();
    private Queue<Object> nextQueue = new();
    private Queue<Object> queue = new();
    private DateTime lastFrame = DateTime.Now;
    private DrawPlanoParameters parameters = new DrawPlanoParameters { X = 0 };
    private DateTime nextSpawnTime = DateTime.Now.AddSeconds(1);
    private Game game;
    public float Velocidade { get; set; }

    public ObjectCollision(float velocidade, Game game) 
    {
        this.Velocidade = velocidade;
        this.game = game; 
    }

    public override void Draw(Graphics g)
    {
        Queue<Object> newQueue = new Queue<Object>();

        if (!newQueue.Any())
            refillQueue();

        if (queue.Any())
        {
            foreach (var obj in queue)
            {
                obj.UpdateAnimation();
                obj.Draw(g);
                obj.X -= Velocidade * deltaTime();

                if (Collision.Current.CheckCollisions(obj))
                {
                    obj.X = 2000;
                    game.TogglePause();
                }
                else if (obj.X + obj.Width < 0)
                {
                    obj.X = 2000;
                }
                else
                {
                    newQueue.Enqueue(obj);
                }
            }
            queue = newQueue;
            SetNextSpawnTime();
        }
    }

    private void SetNextSpawnTime()
    {
        int seconds = Random.Shared.Next(1, 3);
        nextSpawnTime = DateTime.Now.AddSeconds(seconds);
    }

    private float deltaTime()
    {
        var newFrame = DateTime.Now;
        var time = newFrame - lastFrame;
        lastFrame = newFrame;

        return (float)time.TotalSeconds;
    }

    private void refillQueue()
    {
        int objectsCount = Objects.Count;
        while (queue.Count < 1)
        {
            if (nextQueue.Count == 0)
                genNextQueue();

            var next = nextQueue.Dequeue();
            queue.Enqueue(next);
        }
    }

    private void genNextQueue()
    {
        int initialX = Random.Shared.Next(2000, 3000);;
        int y = Random.Shared.Next(730, 740);

        foreach (var obj in Objects.OrderBy(p => Random.Shared.Next()))
        {
            obj.Y = y;
            obj.X = initialX;
            nextQueue.Enqueue(obj);
            initialX += Random.Shared.Next(300, 600);
        }
    }

    public void AddCollisions(Object objects) => this.Objects.Add(objects);
}