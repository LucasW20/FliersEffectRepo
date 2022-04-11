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

	//called by other methods to change the animation of the player. also passes a bool to determine whether or not to change direction
	public static void playPlayerAnimation(string animationName, bool flipDirection) {
		//make sure that there's a current animation
		if (animationName.Equals(currAnimation)) { return; }
		//Console.WriteLine("Start " + animationName);
		
		//try to change the animation will throw an exception if a invalid name is passed
		try {
			//sets the correct direction and visibility
			playerNode.GetNode<Sprite>(currAnimation + "Sprite").Visible = false;
			playerNode.GetNode<Sprite>(animationName + "Sprite").FlipH = flipDirection;
			playerNode.GetNode<Sprite>(animationName + "Sprite").Visible = true;

			//finally changes the current animation variable to the new one and plays the animation
			currAnimation = animationName;
			playerAni.Play(animationName);
		} catch (NullReferenceException e) {
			Console.WriteLine("Invalid Animation Name: ! " + animationName + "\n" + e.Message);
		} catch (Exception e) {
			Console.WriteLine("Unexpected Exception Thrown! " + e.Message);
		}
	}

	//getter for the current animation
	public static string currentAnimationPlaying() {
		return currAnimation;
	}
}
