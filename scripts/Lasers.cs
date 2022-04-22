using Godot;
using System;

public class Lasers : Node2D
{
    private int speed;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        speed = 1700;   
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.MoveLocalX((delta * speed));
    }

    private void LaserReset()
    {
        Position = new Vector2(Position.x - 0, Position.y);
    }

    private void PlayerRespawn(Node body)
    {
        if (body.Name.Equals("TempPlayer"))
        {
            //Console.WriteLine(body.Name);
            //MovingPlatforms myPlat = (MovingPlatforms)body.GetParent();
            //myPlat.ResetPlatform();
            TempPlayer myPlayer = (TempPlayer)body;
            myPlayer.L2Checkpoint();
        }
    }
}
