using Godot;
using System;

/**
 * Handles the player animations switching 
 * @author Lucas_C_Wright
 * @start 04-04-2022
 * @version 04-04-2022
 */
public static class AnimationController {
    private static AnimationPlayer playerAni;
    private static Node2D playerNode;
    private static string currAnimation;

    public static void start(Node2D player) {
        playerAni = player.GetChild<AnimationPlayer>(0);
        playerNode = player;
        currAnimation = "Idle";

    }

    public static void playPlayerAnimation(string animationName) {
        
        switch (animationName) {
            case "Running":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = true;
                break;
            case "Idle":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = true;
                break;
            case "JumpStart":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = true;
                break;
            case "JumpFall":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = true;
                break;
            default:
                GD.Print("Invalid Player Animation Name! " + animationName);
                Console.WriteLine("Invalid Player Animation Name! " + animationName);
                return;
        }

        currAnimation = animationName;
        playerAni.Play(animationName);
    }

    private static void stopPlayerAnimation() {
        playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = false;
    }
}
