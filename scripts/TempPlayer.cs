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
	[Export] public int walkSpeed = 400;
	[Export] public int acc = 1;
	[Export] public float maxSpeed;
	private bool TimeTraveled;
	private Vector2 velocity;
	private Node2D myNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		walkSpeed = 20;
		maxSpeed = 400f;
		TimeTraveled = false;
		jumpHeight = false;
		jumpRelease = false;
		velocity = new Vector2();
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
		jumpTimer = GetNode<Timer>("JumpTimer");
	}

	//runs physics checks every frame 
	public override void _PhysicsProcess(float delta) {
		JumpPhysicsProcess(delta);

		//walking input
		if (Input.IsActionPressed("left")) {
			if (velocity.x > -maxSpeed) {
				if (velocity.x > 0) { //check for swap
					//if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -1;
				} else {
					//apply speed
					velocity.x = velocity.x - acc * walkSpeed;
				}
			}
		} else if (Input.IsActionPressed("right")) {
			if (velocity.x < maxSpeed) {
				if (velocity.x < 0) { //check for swap
					//if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -1;
				} else {
					//apply speed
					velocity.x = velocity.x + acc * walkSpeed;
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

		////down input REPLACED FOR BETTER JUMP CONTROL
		//if (Input.IsActionPressed("down")) {
		//	if (!IsOnFloor()) { //if the player is in the air then they can press 'S' to shoot down at the cost of some horizontal movement
		//		velocity.x -= velocity.x * 0.25f;
		//		velocity.y = 500;
		//  }
		//}

		//Console.WriteLine("<" + velocity.x + ", " + velocity.y + ">");
		
		//actually move the player
		MoveAndSlide(velocity, new Vector2(0, -1));
	}
	
	//gravity and jump variables
	private const float NORMAL_GRAVITY = 750;
	private const  float FAST_FALL_GRAVITY = 1500;
	private const float JUMP = -500;
	private bool jumpHeight;
	private bool jumpRelease;
	private Timer jumpTimer;

	/* There are two systems for a better jump.
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
			if (!jumpHeight || !jumpRelease) { //if they're just falling normally use normal gravity
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
				jumpHeight = false;
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
		jumpHeight = true;
		jumpTimer.Stop();
	}
}
