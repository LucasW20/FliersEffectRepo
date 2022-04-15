using Godot;
using System;

public class PlatformResetV2 : Node2D
{
    //private Node2D myNode;

    //public override void _Ready()
    //{
    //    myNode = GetParent().GetNode<Node2D>("Future moving Platforms");
    //}
    private void Reset(Node body)
	{

        if (!body.Name.Equals("TempPlayer"))
        {
            Console.WriteLine(body.Name);
            MovingPlatforms myPlat = (MovingPlatforms)body.GetParent();
            myPlat.ResetPlatform();
        }
        //if (!body.GetType().Name.Equals("TempPlayer"))
        //{
        //	myNode.Position = new Vector2(-4910, 1746);
        //}

    }
}
