using Godot;
using System;

/**
 * Handles the animations switching on the player and NPCs 
 * @author Lucas_C_Wright
 * @start 04-04-2022
 * @version 04-08-2022
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

	public static void playPlayerAnimation(string animationName, bool flipDirection) {
		if (animationName.Equals(currAnimation)) { return; }
		//Console.WriteLine("Start " + animationName);
		
		try {
			playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = false;
			playerNode.GetNode<Sprite>(animationName + "Sprite").FlipH = flipDirection;
			playerNode.GetNode<Sprite>(animationName + "Sprite").Visible = true;

			currAnimation = animationName;
			playerAni.Play(animationName);
		} catch (NullReferenceException e) {
			Console.WriteLine("Invalid Animation Name: ! " + animationName + "\n" + e.Message);
		} catch (Exception e) {
			Console.WriteLine("Unknown Exception Thrown! " + e.Message);
		}
	}

	public static string currentAnimationPlaying() {
		return currAnimation;
	}
}
