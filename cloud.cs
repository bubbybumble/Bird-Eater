using Godot;
using System;

public class cloud : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
 


    public override void _PhysicsProcess(float delta)
    {
        Position -= new Vector2(Game.timeScale, 0);
        if (Position.x < -1000)
        {
            QueueFree();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
