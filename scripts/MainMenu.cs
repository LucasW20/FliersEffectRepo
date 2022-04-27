using Godot;
using System;

/**
 * Handles the button interactions for the Main Menu screen
 * @author Jaden_Patten
 * @editor Lucas_C_Wright
 * @version 04-27-2022
 */
public class MainMenu : Control
{
	private Timer myTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myTimer = GetNode<Timer>("Timer");
	}

	private void OnStartPressed()
	{
		StartGame();
	}

	async private void StartGame() {
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Stop();
		myTimer.Start();
		await ToSignal(myTimer, "timeout");

		//SWAP TO LOADING IMG
		GetNode<Node2D>("Front").Visible = false;
		myTimer.Start();

		//wait for the timer to finish
		await ToSignal(myTimer, "timeout");

		//CHANGE SCENE
		GetTree().ChangeScene("res://scenes/Levels/Past/Level 1 Past.tscn");
	}

	private void OnOptionsPressed() 
	{
		//Console.WriteLine("Options Button was pressed");
	}

	private void OnExitPressed()
	{
		//Console.WriteLine("Exit Button was pressed");
		GetTree().Quit();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	public void onTimerTimeout() { } //do nothing
}
