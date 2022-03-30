using Godot;
using System;

public class TempEnemyMovement : KinematicBody2D
{
    Sprite sprite;
    RayCast2D bottomLeft;
    RayCast2D bottomRight;
    RayCast2D dirLeft;
    RayCast2D dirRight;

    private Vector2 velocity;
    private int gravity = 200;
    private int speed = 100;

    public override void _Ready()
    {
        sprite = GetNode<Sprite>("Sprite");
        bottomLeft = GetNode<RayCast2D>("LeftRayCast");
        bottomRight = GetNode<RayCast2D>("RightRayCast");
        dirLeft = GetNode<RayCast2D>("LeftDirRayCast");
        dirRight = GetNode<RayCast2D>("RightDirRayCast");
        velocity.x = speed;
    }
    public override void _Process(float delta)
    {
        velocity.y += gravity * delta;
        if (velocity.y > gravity)
        {
            velocity.y = gravity;
        }
        if (!bottomRight.IsColliding())
        {
            velocity.x = -speed;
            sprite.FlipH = false;
        }
        else if (!bottomLeft.IsColliding())
        {
            velocity.x = speed;
            sprite.FlipH = true;
        }
        else if (dirRight.IsColliding())
        {
            velocity.x = -speed;
            sprite.FlipH = false;
        }
        else if (dirLeft.IsColliding())
        {
            velocity.x = speed;
            sprite.FlipH = true;
        }
        MoveAndSlide(velocity, Vector2.Up);
    }

    public void _on_Area2D_body_entered(object body)
    {
        if (body is TempPlayer)
        {
            TempPlayer player = body as TempPlayer;
            player.GetTree().ReloadCurrentScene();
        }
    }
}
