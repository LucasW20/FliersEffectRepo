//using Godot;
//using System;

///***
// * Handles the physics and input of the player jump
// * @author Lucas_C_Wright
// * @start 03-28-2022
// * @version 03-28-2022
// */
//public class JumpPhysics : Node {
//	//gravity and jump variables
//	private const float NORMAL_GRAVITY = 750;
//	private const float FAST_FALL_GRAVITY = 2000;
//	private const float JUMP = -500;
//	private bool jumpRelease;
//	private Timer jumpTimer;

//	/** There are two systems for a better jump.
//	* 1. Max Height. When the player reaches the maximum height of the jump (determined by the timer) gravity is
//	* switched to the FAST_FALL_GRAVITY. This is to make jumps feel less floaty. Uses timer and jumpHeight bool 
//	* to determine when to swap the gravity. Reset jumpHeight when the player presses the jump button. 
//	* 2. Release Jump. When the player releases the jump button then the gravity is switched to the FAST_FALL_GRAVITY.
//	* This is to give better jump control to the player. Uses jumpRelease bool to determine when the button is released
//	* and to switch to FAST_FALL_GRAVITY. Reset jumpRelease to false when player presses the jump button. 
//	*/
//	public Vector2 JumpInput(TempPlayer myPlayer, Vector2 velocity, float delta) {
//		//Vertical movement code. Apply gravity. Only apply while their in the air.
//		if (!myPlayer.IsOnFloor()) {
//			if (!jumpRelease) { //if they're just falling normally use normal gravity
//				velocity.y += NORMAL_GRAVITY * delta;
//			} else { //if they've jumped after a time or released jump button use faster gravity
//				velocity.y += FAST_FALL_GRAVITY * delta;
//			}
//		}

//		if (myPlayer.IsOnFloor()) {
//			jumpRelease = false;
//		}

//		//When the player hits the ceiling make sure they don't dangle.
//		if (myPlayer.IsOnCeiling()) {
//			//Console.WriteLine("On Ceiling");
//			velocity.y *= 0;
//			velocity.y += FAST_FALL_GRAVITY * delta;
//		}

//		//when the player presses the jump button initiate the jump by changing the velocity
//		if (Input.IsActionJustPressed("jump")) {
//			if (myPlayer.IsOnFloor()) { //cant jump while on the ground
//				velocity.y = JUMP;
//				//setup for jump control
//				jumpRelease = false;
//				jumpTimer.Start();
//			}
//		}

//		//when the player releases the jump button swap over to the fast fall gravity
//		if (Input.IsActionJustReleased("jump")) {
//			if (!myPlayer.IsOnFloor()) {
//				//Console.WriteLine("Jump Input released! Switching to Fast Fall Gravity");
//				jumpRelease = true;
//				velocity.y *= 0.6f;
//			}
//		}

//		return velocity;
//    }

////  // Called every frame. 'delta' is the elapsed time since the previous frame.
////  public override void _Process(float delta)
////  {
////      
////  }
//}
