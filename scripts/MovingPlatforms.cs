using Godot;
using System;

public class MovingPlatforms : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int speed;
    //private int XReset;
   // private Node2D myNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //speed = -500;
        speed = -1500;//testing
       // XReset = 0;
        //myNode = GetParent().GetNode<Node2D>("MovingPlatforms");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.MoveLocalX((delta * speed));
    }

    public void ResetPlatform()
    {
        //.Position.x = XReset;
        Console.WriteLine("ResetPlatform Called");
        Position = new Vector2(Position.x + 48000, Position.y);
        
        //Position = new Vector2(0,0);
        //Console.WriteLine("ResetPlatform Active");
        Console.WriteLine(Position);

    }
}
