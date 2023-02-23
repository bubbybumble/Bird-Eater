using Godot;
using System;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool started = false;
    private float birdSpawnCounter = 1f;
    private float planeSpawnCounter = 5f;
    private float barrelSpawnCounter = 2f;
    private float cloudTimer = 2f;
    public static float timeScale = 1f;
    PackedScene birdScene;
    PackedScene planeScene;
    PackedScene barrelScene;
    PackedScene cloudScene;
    CanvasItem startScreen;
    RandomNumberGenerator rng;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize();
       
        birdScene = ResourceLoader.Load<PackedScene>("res://bird.tscn");
        planeScene = ResourceLoader.Load<PackedScene>("res://plane.tscn");
        barrelScene = ResourceLoader.Load<PackedScene>("res://barrel.tscn");
        cloudScene = ResourceLoader.Load<PackedScene>("res://cloud.tscn");

        startScreen = GetTree().Root.GetNode<Label>("Game/Camera2D/CanvasLayer/gameOver/gameover");

        GetTree().Root.GetNode("Singleton").Call("GameLoaded");
    }


    public override void _Process(float delta)
    {
        if (started)
        {
            birdSpawnCounter -= delta * (timeScale * (timeScale /5) +1);
            planeSpawnCounter -= delta * (timeScale * (timeScale / 5) +1);
            barrelSpawnCounter -= delta * (timeScale * (timeScale / 5) + 1);
            timeScale += delta/45;
        }

        cloudTimer -= delta * timeScale;

        if (birdSpawnCounter <= 0f)
        {
            SpawnBird();
            birdSpawnCounter = rng.RandfRange(1/timeScale, 1/timeScale + .6f);
        }
        if (planeSpawnCounter <= 0f)
        {
            SpawnPlane();
            planeSpawnCounter = rng.RandfRange(1/timeScale * 7f, 1/timeScale * 15f);
        }
        if (barrelSpawnCounter <= 0f)
        {
            SpawnBarrel();
            barrelSpawnCounter = rng.RandfRange(1 / timeScale * 7f, 1 / timeScale * 10f);
        }
        if (cloudTimer <= 0f)
        {
            SpawnCloud();
            cloudTimer = rng.RandfRange(1 / timeScale, 1 / timeScale + 1f); ;
        }

    }

    public override void _Input(InputEvent inputEvent)
    {
        if (!started && inputEvent.IsActionPressed("jump"))
        {
            ((CanvasItem)startScreen.GetParent()).Hide();
            started = true;
            GetTree().Root.GetNode<AudioStreamPlayer2D>("Game/AudioStreamPlayer2D").Playing = true;
        }
    }

    private void SpawnBird()
    {
        bird newBird = birdScene.Instance<bird>();
        GetTree().Root.GetNode<Node2D>("Game").AddChild(newBird);
        
        
    }
    private void SpawnPlane()
    {
        plane newPlane = planeScene.Instance<plane>();
        GetTree().Root.GetNode<Node2D>("Game").AddChild(newPlane);
        newPlane.Position = new Vector2(500, rng.RandiRange(-200, -140));
    }
    private void SpawnBarrel()
    {
        barrel newBarrel = barrelScene.Instance<barrel>();
        GetTree().Root.GetNode<Node2D>("Game").AddChild(newBarrel);
        newBarrel.Position = new Vector2(500, 3);
    }
    private void SpawnCloud()
    {
        cloud newCloud = cloudScene.Instance<cloud>();
        GetTree().Root.GetNode<Node2D>("Game").AddChild(newCloud);
        newCloud.Position = new Vector2(500, rng.RandiRange(-250, -120));
    }
}
