using Godot;
using System;

public class MainMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    private void OnStartPressed()
    {
        Console.WriteLine("Start Button was pressed");
        GetTree().ChangeScene("res://scenes/MainScene.tscn");
    }

    private void OnOptionsPressed()
    {
        Console.WriteLine("Options Button was pressed");
    }

    private void OnExitPressed()
    {
        Console.WriteLine("Exit Button was pressed");
        GetTree().Quit();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
