using Godot;
using System;

public class FollowPlayer : Node2D
{
    public override void _Process(float delta)
    {
        var player = GetNode<TempPlayer>("../TempPlayer");
        float speed = 100;
        float movement = speed * delta;

        Vector2 moveDirection = player.Position;
        Position += moveDirection * movement;

        Sprite sprite = GetNode<Sprite>("AnimatedSprite");
        sprite.Position = sprite.Position.LinearInterpolate(moveDirection, delta * speed);


        //if (sprite.Position.x > player.Position.x)
        //{
        //    sprite.FlipH = true;
        //}
        //else if (sprite.Position.x < player.Position.x)
        //{
        //    sprite.FlipH = false;
        //}
    }
}
