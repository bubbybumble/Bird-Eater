using Godot;
using System;

public class floor : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private float x = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        x += 2f * Game.timeScale;
        GetNode<Sprite>("Sprite").RegionRect = new Rect2(x, 0, 1000, 50);
        if(x >= 63)
        {
            x = 0;
        }
    }
}
