using Godot;
using System;

public class plane : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    RandomNumberGenerator rng;
    int speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize();

        speed = rng.RandiRange(230, 320);
    }

    public override void _PhysicsProcess(float delta)
    {
        Position -= new Vector2(delta * speed * (1/2 + Game.timeScale/2), 0);
        if (Position.x < -1000)
        {
            QueueFree();
        }
    }
}