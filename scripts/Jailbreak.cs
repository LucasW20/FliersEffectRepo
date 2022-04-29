using Godot;
using System;

public class Jailbreak : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool JailLocked = true;
    public Vector2 Loc = new Vector2 { };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void Breakout(bool jail)
    {
        Loc = new Vector2 { };
        if(JailLocked == true)
        {
            Console.WriteLine("Breakout Called");
            JailLocked = false;
           Visible = false;
           
            

            //GetChild(0).SetPhysicsProcess(false);
           
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
