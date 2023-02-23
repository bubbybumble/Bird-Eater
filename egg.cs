using Godot;
using System;

public class egg : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        Position -= new Vector2(2 * Game.timeScale, 0); //2 is speed of floor

    }

    private void _on_egg_body_entered(PhysicsBody2D a)
    {
        if(a is Guy)
        {
            a.Call("GameOver");
            GetNode<AnimatedSprite>("AnimatedSprite").Play("explode");
        }
    }


}
