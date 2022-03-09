using Godot;
using System;

/***
 * Handles the first iteration of player movement for Fliers Effect
 */
public class TempPlayer : KinematicBody2D {
	[Export] public int walkSpeed = 200;
	private const float gravity = 200.0f;
	private bool TimeTraveled;
	public Vector2 velocity = new Vector2();
	Node2D myNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		TimeTraveled = false;
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
	}

	public override void _PhysicsProcess(float delta) {
		float walk = walkSpeed * (Input.GetActionStrength("right") - Input.GetActionStrength("left"));

		//Vertical movement code. Apply gravity.
		velocity.y += gravity * delta;

		//walking input
		if (Input.IsActionPressed("left")) {
			velocity.x = -walkSpeed;
		} else if (Input.IsActionPressed("right")) {
			velocity.x = walkSpeed;
		} else {
			velocity.x = 0;
		}

		//time travel input
		if (Input.IsActionJustPressed("timetravel")) {
			Console.WriteLine("Pressed/n");
			if (TimeTraveled == false) { //if in the future then move to the past
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y - 200);
				TimeTraveled = true;
			} else { //if in the past then move to the future
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y + 200);
				TimeTraveled = false;
			}
		}

		//actually move the player
		MoveAndSlide(velocity, new Vector2(0, -1));
	}
}
