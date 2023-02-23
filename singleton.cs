using Godot;
using System;

public class singleton : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public static int highScore = 0;
    private Label highScoreBoard;
    Timer timer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PauseMode = PauseModeEnum.Process;
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public void GameOver()
    {
        Game.timeScale = 1f;
        GetTree().Root.GetNode<AudioStreamPlayer2D>("Game/gameoversound").Play();
        GetTree().Paused = true;
        timer = new Timer();
        timer.Connect("timeout", this, "_on_timer_timeout");
        AddChild(timer);
        timer.Start(4f);
        
    }

    public void GameLoaded()
    {
        highScoreBoard = GetTree().Root.GetNode<Label>("Game/Camera2D/CanvasLayer/gameOver/gameover");
        
        GD.Print(highScore);
        highScoreBoard.Text =
            "tap space to jump, tap w to open mouth\n" +
            "eat birds, avoid planes and barrels\n\n" +
           "PRESS SPACE TO START\n\n" +
           "HIGH SCORE: " + highScore;
    }

    public void _on_timer_timeout()
    {
        timer.QueueFree();
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
       
    }
}
