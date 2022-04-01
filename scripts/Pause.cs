using Godot;
using System;

/**
 * first iteration of pause menu. pauses the game and lets the player access menus.
 * @author Jaden_Patten
 * @start 03-14-2022
 * @version 03-14-2022
 */
public class Pause : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public bool PauseState;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Console.WriteLine("Pause script accessed");
		PauseState = false;
		
	}

	//pauses the game
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			Pausing(!GetTree().Paused);
		}
	}

	private void OnContinuePressed()
	{
		Console.WriteLine("Continue Pressed");
		Pausing(false);
	}

	private void OnOptionsPressed()
	{
		Console.WriteLine("Options Pressed");
	}

	private void OnRestartPressed()
	{
		Console.WriteLine("Restart Pressed");
		Pausing(false);

		GetTree().ChangeScene("res://scenes/MainScene.tscn");
	}

	private void OnMainMenuPressed()
	{
		Console.WriteLine("Main Menu Pressed");
		Pausing(false);
		GetTree().ChangeScene("res://scenes/MainMenu.tscn");

	}

	public void Pausing(bool pause)
	{
		PauseState = pause;
		GetTree().Paused = PauseState;
		Console.WriteLine("pause toggled");
		Console.WriteLine(PauseState);
		Visible = PauseState;
	}

	
}
