using Godot;
using System;

/**
 * Handles the player animations switching 
 * @author Lucas_C_Wright
 * @start 04-04-2022
 * @version 04-06-2022
 */
public static class AnimationController {
    private static AnimationPlayer playerAni;
    private static Node2D playerNode;
    private static string currAnimation;

    public static void start(Node2D player) {
        playerAni = player.GetChild<AnimationPlayer>(0);
        playerNode = player;
        currAnimation = "Idle";
        playerAni.Play("Idle");
    }

    public static void playPlayerAnimation(string animationName) {
        if (animationName.Equals(currAnimation)) { return; }
        Console.WriteLine("Start " + animationName);

        switch (animationName) {
            case "Running":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>("RunningSprite").Visible = true;
                break;
            case "Idle":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>("IdleSprite").Visible = true;
                break;
            //case "JumpStart":
            //    stopPlayerAnimation();
            //    playerNode.GetNode<Sprite>("JumpStartSprite").Visible = true;
            //    break;
            case "JumpFall":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>("JumpFallSprite").Visible = true;
                break;
            case "Jumping":
                stopPlayerAnimation();
                playerNode.GetNode<Sprite>("JumpingSprite").Visible = true;
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
        Console.WriteLine("Stopping " + currAnimation);
        playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = false;
    }

    public static string currentAnimationPlaying() {
        return currAnimation;
    }
}