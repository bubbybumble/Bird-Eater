using Godot;
using System;

public class Guy : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int score = 0;
    
    private float grav = 9.81f;
    private float velocity = 1f;
    private float jumpVel = -275f;
    private float scale = 1f;
    private Node2D mouth;
    private Node2D head;
    private Node2D mouthCollision;
    private Label scoreBoard;
    

    private float scaleVel = 0f;

    public override void _Ready()
    {
        mouth = GetNode<Node2D>("mouth");
        head = GetNode<Node2D>("head");
        scoreBoard = GetTree().Root.GetNode<Label>("Game/Camera2D/CanvasLayer/MarginContainer/score");

    }


    public override void _PhysicsProcess(float delta)
    {
       

        //mouth controls (yummy)
        
        if (Input.IsActionJustPressed("up"))
        {
            if(scale < 4)
            {
                scaleVel = 7f;
            }
            
        }
        else
        {
            if(scale > 0f)
            {
                scaleVel -= delta * grav * 3f;
            }
            
        }
        
        scale += scaleVel * delta;

        scale = Math.Max(scale, 0);
        scale = Math.Min(scale, 4);

        mouth.Scale = new Vector2(1, scale);
        mouth.Position = new Vector2(0, scale * -50/2 -25);

        head.Position = new Vector2(0,-50 - 50 * scale);

        //jumping and gravity
        MoveAndSlide(new Vector2(0, velocity),Vector2.Up);
        if (IsOnFloor())
        {
            velocity = 1f;
            if (Input.IsActionJustPressed("jump"))
            {
                velocity = jumpVel;
            }
        }
        else
        {
            velocity += grav;
        }
        
    }

    public void _on_Area2D_area_entered(Area2D a)
    {
        if (a is bird)
        {
            ((bird)a).eaten = true;
            Particles2D p = a.GetNode<Particles2D>("feathers");
            
           
            a.GetNode<Node2D>("CollisionShape2D").QueueFree();
            a.GetNode<Node2D>("AnimatedSprite").QueueFree();
            p.Emitting = true;

            score += ((bird)a).worth;
            singleton.highScore = Math.Max(score, singleton.highScore);
            scoreBoard.Text = "SCORE: " + score;
            GetTree().Root.GetNode<AudioStreamPlayer2D>("Game/chomp").Play();

        }
        if (a is plane)
        {
            a.GetNode<AnimatedSprite>("AnimatedSprite").Play("boom");
            GameOver();
        }
    }

    public void _on_HeadArea_area_entered(Area2D a)
    {
        if (a is bird)
        {
            GetTree().Root.GetNode<AudioStreamPlayer2D>("Game/whack").Play();
            a.GetNode<Node2D>("CollisionShape2D").QueueFree();
            ((bird)a).bonked = true;
        }
        if (a is plane)
        {
            a.GetNode<AnimatedSprite>("AnimatedSprite").Play("boom");
            GameOver();
        }
    }



    public void GameOver()
    {
        GetNode<AnimatedSprite>("head").Play("dead");
        GetTree().Root.GetNode("Singleton").Call("GameOver"); 
       
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

internal class ParticleEmitter2D
{
}