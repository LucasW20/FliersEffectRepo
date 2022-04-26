using Godot;
using System;

/***
 * Handles the first iteration of player movement for Fliers Effect and the time travel
 * @author Jaden_Patten
 * @editor Lucas_C_Wright
 * @start 04-25-2022
 * @version 04-25-2022
 */
public class RespawnFade : Sprite
{
	//private Sprite fadeImage;
	private Timer exitTimer;

	public override void _Ready()
	{
		//fadeImage = GetParent().GetNode<Sprite>("Black");
		exitTimer = GetChild<Timer>(0);
	}

	//called by other scripts to fade out and respawn the player
	public void SpawnFade(Node2D respawnee, Vector2 nPos) { FadeOut(respawnee, nPos); }

	//async method that actually fades out the screen and moves the player
	async private void FadeOut(Node2D rNode, Vector2 nPos)
	{
		Modulate = new Color(Modulate, 0); //make sure the alpha of the image is 0

		//loop to gradually change the the alpha value by 0.025 each iteration
		for (float i = 0; i <= 1; i += 0.025f)
		{
			//change the alpha
			Modulate = new Color(Modulate, i);
			//Console.WriteLine(Modulate.a);

			//wait for the timer to finish so the fade doesn't look instant. Timer takes 0.05 seconds to finish
			exitTimer.Start();
			await ToSignal(exitTimer, "timeout");
		}

		//move the rNode to the new location
		rNode.Position = nPos;

		//after moving loop to fade back in
		for (float i = 1; i > 0; i -= 0.025f) {
			//change the alpha
			Modulate = new Color(Modulate, i);
			//Console.WriteLine(Modulate.a);

			//wait for the timer to finish so the fade doesn't look instant. Timer takes 0.05 seconds to finish
			exitTimer.Start();
			await ToSignal(exitTimer, "timeout");
		}
	}
}
