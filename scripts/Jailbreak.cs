using Godot;
using System;

public class Jailbreak : KinematicBody2D
{
    private bool JailLocked = true;
    public Vector2 Loc;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void Breakout(Node2D body) {
        Loc = new Vector2(40000, 0);

        if(JailLocked) {
            Console.WriteLine("Breakout Called");
            JailLocked = false;
            Visible = false;
            Position = Loc;
            //GetChild(0).SetPhysicsProcess(false);
        }
    }
}
