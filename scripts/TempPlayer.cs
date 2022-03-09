using Godot;
using System;

/***
 * Handles the first iteration of player movement for Fliers Effect and the time travel
 * @author Lucas_C_Wright
 * @editor Jaden_Patten
 * @start 3-7-2022
 * @version 3-9-2022
 */
public class TempPlayer : KinematicBody2D {
	private const float GRAVITY = 200.0f;
	private int walkSpeed;
	private bool TimeTraveled;
	private Vector2 velocity;
	private Node2D myNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		walkSpeed = 200;
		TimeTraveled = false;
		velocity = new Vector2();
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
	}

	//runs physics checks every frame 
	public override void _PhysicsProcess(float delta) {
		//Vertical movement code. Apply gravity.
		velocity.y += GRAVITY * delta;

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
			//Console.WriteLine("Pressed/n");
			if (TimeTraveled == false) { //if in the future then move to the past
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y - 300);
				TimeTraveled = true;
			} else { //if in the past then move to the future
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y + 300);
				TimeTraveled = false;
			}
		}

		//actually move the player
		MoveAndSlide(velocity, new Vector2(0, -1));
	}
}
