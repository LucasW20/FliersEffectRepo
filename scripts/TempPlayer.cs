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
	[Export] public float GRAVITY = 750;
	[Export] public float JUMP = -500;
	[Export] public int walkSpeed = 400;
	[Export] public int acc = 1;
	private bool TimeTraveled;
	private Vector2 velocity;
	private Node2D myNode;
	[Export] public float maxSpeed;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		walkSpeed = 20;
		maxSpeed = 400f;
		TimeTraveled = false;		
		velocity = new Vector2();
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
	}

	//runs physics checks every frame 
	public override void _PhysicsProcess(float delta) {
		//Vertical movement code. Apply gravity.
		if (!IsOnFloor()) { velocity.y += GRAVITY * delta; }

		//walking input
		if (Input.IsActionPressed("left")) {
			if (velocity.x > -maxSpeed) { 
				if (velocity.x > 0) { velocity.x *= -1; }
				velocity.x = velocity.x - acc * walkSpeed;
			}
		} else if (Input.IsActionPressed("right")) {
			if (velocity.x < maxSpeed) {
				if (velocity.x < 0) { velocity.x *= -1; }
				velocity.x = velocity.x + acc * walkSpeed;
			}
		} else {
			velocity.x = 0;
		}

		//jumping input
		if (Input.IsActionPressed("jump")) {
			//Console.WriteLine("Pressed Jump");
			if (IsOnFloor()) {
				//Console.WriteLine("Jumped");
				velocity.y = JUMP;
            }
        }

		//down input
		if (Input.IsActionPressed("down")) {
			if (!IsOnFloor()) { //if the player is in the air then they can press 'S' to shoot down at the cost of some horizontal movement
				velocity.x -= velocity.x * 0.25f;
				velocity.y = 500;
            }
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

		Console.WriteLine("<" + velocity.x + ", " + velocity.y + ">");
		//actually move the player
		MoveAndSlide(velocity, new Vector2(0, -1));
	}
}
