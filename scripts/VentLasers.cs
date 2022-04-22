using Godot;
using System;

public class VentLasers : Node2D
{
   private void PlayerRespawn(Node body)
    {
        if (body.Name.Equals("TempPlayer"))
        {
            //Console.WriteLine(body.Name);
            //MovingPlatforms myPlat = (MovingPlatforms)body.GetParent();
            //myPlat.ResetPlatform();
            TempPlayer myPlayer = (TempPlayer)body;
            if(myPlayer.timeTraveled == true)
            {
                myPlayer.timeTraveled = false;
            }
            myPlayer.L2StartSpawn();
        }
    }
}
