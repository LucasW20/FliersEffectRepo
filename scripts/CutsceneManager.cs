using Godot;
using System;

/**
 * Handles the cutscenes
 * @author Lucas_C_Wright
 */
public class CutsceneManager : Node {
	private Timer myTimer;
	private RichTextLabel prompt;
	private string nextScene;
	private bool triggered = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		nextScene = "";
		myTimer = GetNode<Timer>("Timer");
		prompt = GetNode<RichTextLabel>("PromptText");
		prompt.Modulate = new Color(prompt.Modulate, 0);

		//start the loop
		loopFadeText();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		//check for the input break
		if (Input.IsActionJustPressed("jump")) {
			triggered = true;
		}
	}

	async private void loopFadeText() {
		//at the start wait for a moment to get the next scene variable from the timer
		myTimer.Start();
		await ToSignal(myTimer, "timeout");

		while (true) {
			if (triggered) {
				break;
			}

			//fade in text
			for (float i = 0; i <= 1; i += 0.025f) {
				prompt.Modulate = new Color(prompt.Modulate, i);
				myTimer.Start();
				await ToSignal(myTimer, "timeout");
			}

			myTimer.Start();
			await ToSignal(myTimer, "timeout");

			//fade out text
			for (float i = 1; i >= 0; i -= 0.025f) {
				prompt.Modulate = new Color(prompt.Modulate, i);
				myTimer.Start();
				await ToSignal(myTimer, "timeout");
			}
		}

		GetTree().ChangeScene(nextScene);
	}

	private void onTimerTimeout(String extra_arg_0) {
		if (nextScene.Empty()) {
			nextScene = extra_arg_0;
		}
	} 
}
