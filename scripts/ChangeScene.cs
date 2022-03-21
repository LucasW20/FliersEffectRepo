using Godot;
using System;

/***
 * Handles changing the scene
 * @author Lucas_C_Wright
 * @start 3-21-2022
 * @version 3-21-2022
 */
public class ChangeScene : Node {

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	private void OnBodyEntered(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) {
			Console.WriteLine("Entered Area");
			GetTree().ChangeScene("res://scenes/PrisonLevel.tscn");
		}
	}

	private void OnStartButtonPressed() {
		GetTree().ChangeScene("res://scenes/MainScene.tscn");
	}

}








