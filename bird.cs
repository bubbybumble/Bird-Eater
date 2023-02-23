using Godot;
using System;

public class bird : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int choice;
    public int worth = 1;
    RandomNumberGenerator rng;
    AnimatedSprite sprite;
    private float startY = 0f;
    int speed;
    float amp;
    float per;
    public bool bonked = false;
    public bool eaten = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        rng = new RandomNumberGenerator();
        rng.Randomize();
        int rand = rng.RandiRange(1, 6);
        if (rand == 1)
        {
            choice = 3;
        } 
        else if (rand <= 3)
        {
            choice = 2;
        }
        else
        {
            choice = 1;
        }


        switch (choice)
        {
            case 1: 
                sprite.Play("blue");
                GetNode<Particles2D>("feathers").Modulate = new Color(.2f, .2f, 1);
                worth = 1;
                amp = rng.RandfRange(0, 15);
                per = rng.RandfRange(130, 450) / (float)Math.PI;
                speed = rng.RandiRange(200, 300);
                Position = new Vector2(500, rng.RandiRange(-100, -50));
                break;
            case 2: 
                sprite.Play("red");
                GetNode<Particles2D>("feathers").Modulate = new Color(1, .2f, .2f);
                worth = 2;
                amp = rng.RandfRange(15, 25);
                per = rng.RandfRange(110, 500) / (float)Math.PI;
                speed = rng.RandiRange(220, 350);
                Position = new Vector2(500, rng.RandiRange(-150, -120));
                break;
            case 3: 
                sprite.Play("gold");
                GetNode<Particles2D>("feathers").Modulate = new Color(1, .9f, .2f);
                worth = 4;
                amp = rng.RandfRange(25, 50);
                per = rng.RandfRange(100, 300) / (float)Math.PI;
                speed = rng.RandiRange(300, 400);
                Position = new Vector2(500, rng.RandiRange(-200, -160));
                break;
            
        }

        startY = Position.y;


    }

 

    public override void _PhysicsProcess(float delta)
    {
        if (bonked && !eaten)
        {
            Position -= new Vector2(delta * speed * (1 / 2 + Game.timeScale / 2), rng.RandfRange(2,4));
            Rotation += delta*rng.RandfRange(3,8);
        }
        else
        {
            Position = new Vector2(Position.x - delta * speed * (1 / 2 + Game.timeScale / 2), startY - (amp * (float)Math.Sin(Position.x * (1 / per))));

        }
        if (Position.x < -1000)
        {
            QueueFree();
        }

    }
}