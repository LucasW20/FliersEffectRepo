using Godot;
using System;

/***
 * Handles the first iteration of player movement for Fliers Effect and the time travel
 * @author Lucas_C_Wright
 * @editor Jaden_Patten
 * @editor Jake_S
 * @start 3-7-2022
 * @version 3-10-2022
 */
public class TempPlayer : KinematicBody2D {
	private int walkSpeed = 400;
	private int acc = 10;
	private float maxSpeed;
	private bool TimeTraveled;
	private Node2D myNode;

	public Vector2 velocity;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		walkSpeed = 20;
		maxSpeed = 600f;
		TimeTraveled = false;
		jumpRelease = false;
		velocity = new Vector2();
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
		jumpTimer = GetNode<Timer>("JumpTimer");
	}

	Vector2 FLOOR_NORMAL = Vector2.Up;
	const float SNAP_LENGTH = 8.0f;
	Vector2 snapVector = SNAP_LENGTH * Vector2.Down;


	public bool onCurve = false;

	//runs physics checks every frame 
	public override void _PhysicsProcess(float delta) {
		JumpPhysicsProcess(delta);
		DirectionMovementPP(delta);

		Console.WriteLine("<" + velocity.x + ", " + velocity.y + ">");
		//Console.WriteLine(onCurve);

		
		
		//actually move the player
		if (onCurve) { //while the player is on a curve, snap them to the ground
			velocity.y = MoveAndSlideWithSnap(velocity, snapVector, FLOOR_NORMAL, true).y;
		} else { //if they're not then only move and slide so they can jump
			MoveAndSlide(velocity, new Vector2(0, -1));
		}
	}

	private void DirectionMovementPP(float delta) {
		//walking input
		if (Input.IsActionPressed("left")) {
			if (velocity.x > -maxSpeed) {
				if (velocity.x > 0) { //check for swap
					 //if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -1;
				} else {
					//apply speed
					//velocity.x = velocity.x - acc * walkSpeed;
					velocity.x = velocity.x - acc;
				}
			}
		} else if (Input.IsActionPressed("right")) {
			if (velocity.x < maxSpeed) {
				if (velocity.x < 0) { //check for swap
					//if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -1;
				} else {
					//apply speed
					//velocity.x = velocity.x + acc * walkSpeed;
					velocity.x = velocity.x + acc;
				}
			}
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
	}
	
	//gravity and jump variables
	private const float NORMAL_GRAVITY = 750;
	private const  float FAST_FALL_GRAVITY = 2000;
	private const float JUMP = -500;
	private bool jumpRelease;
	private Timer jumpTimer;

	/** There are two systems for a better jump.
	* 1. Max Height. When the player reaches the maximum height of the jump (determined by the timer) gravity is
	* switched to the FAST_FALL_GRAVITY. This is to make jumps feel less floaty. Uses timer and jumpHeight bool 
	* to determine when to swap the gravity. Reset jumpHeight when the player presses the jump button. 
	* 2. Release Jump. When the player releases the jump button then the gravity is switched to the FAST_FALL_GRAVITY.
	* This is to give better jump control to the player. Uses jumpRelease bool to determine when the button is released
	* and to switch to FAST_FALL_GRAVITY. Reset jumpRelease to false when player presses the jump button. 
	*/
	private void JumpPhysicsProcess(float delta) {
		//Vertical movement code. Apply gravity. Only apply while their in the air.
		if (!IsOnFloor()) {
			if (!jumpRelease) { //if they're just falling normally use normal gravity
				velocity.y += NORMAL_GRAVITY * delta;
			} else { //if they've jumped after a time or released jump button use faster gravity
				velocity.y += FAST_FALL_GRAVITY * delta;
			}
		}

		//When the player hits the ceiling make sure they don't dangle.
		if (IsOnCeiling()) {
			//Console.WriteLine("On Ceiling");
			velocity.y *= 0;
			velocity.y += FAST_FALL_GRAVITY * delta;
		}

		//when the player presses the jump button initiate the jump by changing the velocity
		if (Input.IsActionJustPressed("jump")) {
			if (IsOnFloor()) { //cant jump while on the ground
				velocity.y = JUMP;
				//setup for jump control
				jumpRelease = false;
				jumpTimer.Start();
			}
		}

		//when the player releases the jump button swap over to the fast fall gravity
		if (Input.IsActionJustReleased("jump")) {
			if (!IsOnFloor()) { 
				//Console.WriteLine("Jump Input released! Switching to Fast Fall Gravity");
				jumpRelease = true;
				velocity.y *= 0.6f;
			}
		}
	}

	//method for when the timer runs out. 
	private void _on_JumpTimer_timeout() {
		Console.WriteLine("JumpTimer Timeup! Switching to Fast Fall Gravity");
		jumpRelease = true;
		jumpTimer.Stop();
	}
}
