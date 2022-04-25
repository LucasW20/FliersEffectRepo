using Godot;
using System;

public class RespawnFade : Sprite
{
	//private Sprite fadeImage;
	private Timer exitTimer;

	public override void _Ready()
	{
		//fadeImage = GetParent().GetNode<Sprite>("Black");
		exitTimer = GetChild<Timer>(0);
	}

	public void SpawnFade()
    {
		FadeOut();
    }

	async private void FadeOut()
	{
		Modulate = new Color(Modulate, 0); //make sure the alpha of the image is 0

		//loop to gradually change the the alpha value by 0.025 each iteration
		for (float i = 0; i <= 1; i += 0.025f)
		{
			//change the alpha
			Modulate = new Color(Modulate, i);
			Console.WriteLine(Modulate.a);

			//wait for the timer to finish so the fade doesn't look instant. Timer takes 0.05 seconds to finish
			exitTimer.Start();
			await ToSignal(exitTimer, "timeout");
		}

		
	}
}
