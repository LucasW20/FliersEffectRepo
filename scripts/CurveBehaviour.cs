using Godot;
using System;

/***
 * Handles the various behaviours for when the player runs on curves
 * @author Lucas_C_Wright
 * @start 3-26-2022
 * @version 3-27-2022
 */
public class CurveBehaviour : Node {


	//when the player enteres the box collider for the curve set the players onCurve boolean to true for physics processing
	private void OnBodyEnteredTrigger(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) {
			TempPlayer player = (TempPlayer)body;
			player.onCurve = true;
		}
	}

	//changes the players onCurve boolean to false when exiting the collider
	private void OnBodyExitTrigger(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) {
			TempPlayer player = (TempPlayer)body;
			player.onCurve = false;
		}
	}

	//public override void _PhysicsProcess(float delta) {
	//
	//}

	//// Called when the node enters the scene tree for the first time.
	//public override void _Ready() {
	//
	//}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta) {
	//      
	//  }
}
