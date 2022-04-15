using Godot;
using System;

/**
 * Handles the behaviour for when a player passes a booster interactable
 * @author Lucas_C_Wright
 * @start 04-11-2022
 * @version 04-11-2022
 */
public class BoosterBehaviour : Node {

    //called by a singal from the Area2D of the booster node
    public void OnBodyEntered(Node body) {
        if (body.Name.Equals("TempPlayer")) { //ensure that that this singal was triggered by the player's body
            TempPlayer player = (TempPlayer) body;

            //check the direction of the player and apply the correct polarity
            if (player.velocity.x > 0) {
                player.velocity.x += 2000f;
            } else {
                player.velocity.x -= 2000f;
            }
        }
    }
}
