using Godot;
using System;

/***
 * Handles changing the scene
 * @author Lucas_C_Wright
 * @start 3-21-2022
 * @version 3-21-2022
 */
public class ChangeScene : Node {
	private Sprite fadeImage;
	private Timer exitTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		fadeImage = GetChild<Sprite>(0);
		exitTimer = GetChild<Timer>(2);
	}

	//runs when the player enteres the area that exits the scene
	private void OnBodyEntered(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) { //if the player and not another object enters the exit area
			//Console.WriteLine("Entered Area");
			//fade out the scene 
			FadeOut();
		}
	}

	//fades out the scene and then changes the scene
	async private void FadeOut() {
		fadeImage.Modulate = new Color(fadeImage.Modulate, 0); //make sure the alpha of the image is 0

		//loop to gradually change the the alpha value by 0.025 each iteration
		for (float i = 0; i <= 1; i += 0.025f) {
			//change the alpha
			fadeImage.Modulate = new Color(fadeImage.Modulate, i);
			Console.WriteLine(fadeImage.Modulate.a);

			//wait for the timer to finish so the fade doesn't look instant. Timer takes 0.05 seconds to finish
			exitTimer.Start();
			await ToSignal(exitTimer, "timeout");
		}

		//when the fade has completed actually change the scene
		GetTree().ChangeScene("");
	}

	private void OnExitTimerRunout() { } //do nothing
}
