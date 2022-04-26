using Godot;
using System;

/***
 * Handles the first iteration of player movement for Fliers Effect and the time travel
 * @author Lucas_C_Wright
 * @editor Jaden_Patten
 * @editor Jake_S
 * @start 03-07-2022
 * @version 04-20-2022
 */
public class TempPlayer : KinematicBody2D {
	private const float acc = 80;
	private const float dcc = 5;
	private float maxSpeed;
	public bool timeTraveled;
	private Node2D myNode;
	private bool flipDirection = false;	//true means facing left, false means facing right

	public Vector2 velocity;

	//Audio
	private AudioStreamPlayer pastPlayer;
	private AudioStreamPlayer futurePlayer;
	private AudioStreamPlayer jumpAudio;
	private AudioStreamPlayer timeTravelAudio;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		maxSpeed = 4000;
		timeTraveled = false;
		jumpRelease = false;
		velocity = new Vector2();
		myNode = GetParent().GetNode<Node2D>("TempPlayer");
		jumpTimer = GetNode<Timer>("JumpTimer");
		//jump = new JumpPhysics();

		//myNode.GetChild<AnimationPlayer>(0).Play("Idle");
		AnimationController.start(myNode);

		//audio
		pastPlayer = GetParent().GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		futurePlayer = GetParent().GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
		pastPlayer.VolumeDb = 10;
		pastPlayer.Play();
		futurePlayer.VolumeDb = -20;
		futurePlayer.Play();
	}

	//snap movement variables
	private Vector2 FLOOR_NORMAL = Vector2.Up;
	private const float SNAP_LENGTH = 150.0f;
	private Vector2 snapVector = SNAP_LENGTH * Vector2.Down;
	private const float SLOPE_THRESH = 0.802851f;
	private const float SWAP_SPEED = 0.5f;

	//runs physics checks every frame 
	public override void _PhysicsProcess(float delta) {
		//velocity = jump.JumpInput((TempPlayer)myNode, velocity, delta);
		//get jump and directional movement
		JumpPhysicsProcess(delta);
		DirectionMovementPP(delta);

		//move the character so that they snap to the gound and not bounce on slopes
		velocity.y = MoveAndSlideWithSnap(velocity, snapVector, FLOOR_NORMAL, true, 4, SLOPE_THRESH).y;

		//after a jump swap back to the normal snapVector so the player continues to snap to the ground
		if (IsOnFloor() && snapVector == Vector2.Zero) {
			snapVector = SNAP_LENGTH * Vector2.Down;
		}

		if (startJump) {
			AnimationController.playPlayerAnimation("Jumping", flipDirection);
			startJump = false;
		}

		myNode.GetNode<Sprite>(AnimationController.currentAnimationPlaying() + "Sprite").FlipH = flipDirection;
		//Console.WriteLine("<" + velocity.x + ", " + velocity.y + ">");
		//Console.WriteLine(jumpRelease);
		//Console.WriteLine(onCurve);
	}

	private bool startJump = false;
	private void DirectionMovementPP(float delta) {
		//walking input
		if (Input.IsActionPressed("left")) {
			if (IsOnFloor()) { AnimationController.playPlayerAnimation("Running", flipDirection); }


			if (velocity.x > -maxSpeed) {
				if (velocity.x > 0) { //check for swap
					 //if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -SWAP_SPEED;
				} else {
					//apply speed
					//velocity.x = velocity.x - acc * walkSpeed;
					velocity.x -= acc;
				}
			} else if (velocity.x <= -maxSpeed) { //if the player has more speed than the max then apply deceleration 
				velocity.x += dcc;
			}

			//change to facing left
			flipDirection = true;
		} else if (Input.IsActionPressed("right")) {
			if (IsOnFloor()) {
				AnimationController.playPlayerAnimation("Running", flipDirection);
			}
			if (velocity.x < maxSpeed) {
				if (velocity.x < 0) { //check for swap
					//if the player swaps direction then maintain the momentum by swaping the velocity sign
					velocity.x *= -SWAP_SPEED;
				} else {
					//apply speed
					//velocity.x = velocity.x + acc * walkSpeed;
					velocity.x += acc;
				}
			} else if (velocity.x >= maxSpeed) { //if the player has more speed than the max then apply deceleration 
				velocity.x -= dcc;
			}

			//change to facing right
			flipDirection = false;
		} else {
			velocity.x = 0;
			if (IsOnFloor()) {
				AnimationController.playPlayerAnimation("Idle", flipDirection);
			}
		}

		//time travel input
		if (Input.IsActionJustPressed("timetravel")) {
			//Console.WriteLine("Pressed/n");
			if (timeTraveled == false) { //if in the future then move to the past
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y - 50000);
				timeTraveled = true;
				//audio
				timeTravelAudio = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
				timeTravelAudio.PitchScale = 3;
				timeTravelAudio.Play();
				timeTravelAudio.VolumeDb = 20;
				futurePlayer.VolumeDb = 10;
				pastPlayer.VolumeDb = -20;
				
			} else { //if in the past then move to the future
				myNode.Position = new Vector2(myNode.Position.x, myNode.Position.y + 50000);
				timeTraveled = false;
				//audio
				timeTravelAudio = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
				timeTravelAudio.PitchScale = 3;
				timeTravelAudio.Play();
				timeTravelAudio.VolumeDb = 20;
				pastPlayer.VolumeDb = 10;
				futurePlayer.VolumeDb = -20;
			}
		}
	}
	
	//gravity and jump variables
	private const float NORMAL_GRAVITY = 4000;
	private const  float FAST_FALL_GRAVITY = 16000;
	private const float JUMP = -6000;
	private bool jumpRelease;
	private Timer jumpTimer;
	private bool notFalling = false;

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

		if (IsOnFloor()) {
			jumpRelease = false;
			notFalling = false;
		}

		//When the player hits the ceiling make sure they don't dangle.
		if (IsOnCeiling()) {
			//Console.WriteLine("On Ceiling");
			velocity.y *= 0;
			velocity.y += FAST_FALL_GRAVITY * delta;
		}

		//when the player presses the jump button while on the ground initiate the jump by changing the velocity
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) {
			notFalling = true;
			snapVector = Vector2.Zero;
			velocity.y = JUMP;
			//setup for jump control
			jumpRelease = false;
			jumpTimer.Start();

			//jump sound effect
			jumpAudio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
			jumpAudio.Play();
			jumpAudio.VolumeDb = 20;

			startJump = true;
		}

		//when the player releases the jump button swap over to the fast fall gravity
		if (Input.IsActionJustReleased("jump") && !IsOnFloor()) {
			//Console.WriteLine("Jump Input released! Switching to Fast Fall Gravity");
			jumpRelease = true;
			velocity.y *= 0.6f;
		}

		if (!IsOnFloor() && !notFalling) {
			velocity.y += FAST_FALL_GRAVITY * delta;
			AnimationController.playPlayerAnimation("JumpFall", flipDirection);
		}
	}

	//method for when the timer runs out. 
	private void _on_JumpTimer_timeout() {
		//when the timer runs out flip the jumpRelease boolean so that the faster gravity is used instead
		Console.WriteLine("JumpTimer Timeup! Switching to Fast Fall Gravity");
		jumpRelease = true;
		jumpTimer.Stop();
		AnimationController.playPlayerAnimation("JumpFall", flipDirection);
	}

	//Future Respawn method
	private void FRespawn(object body)
	{
		if (body.GetType().Name.Equals("TempPlayer"))
		{
			GetParent().GetNode<RespawnFade>("Black").SpawnFade(myNode, new Vector2(6444, -52044));
		}

	}

	//Past Respawn method
	private void PRespawn(object body)
	{
		if (body.GetType().Name.Equals("TempPlayer"))
		{
			GetParent().GetNode<RespawnFade>("Black").SpawnFade(myNode, new Vector2(-4910, 1746));
		}

	}

	//Past Checkpoint
	private void PCheckpoint(object body)
	{
		myNode.Position = new Vector2(14275, -5227);
	}

	//Future Checkpoint
	//private void FCheckpoint(object body,Vector2 Argument_1)
	private void FCheckpoint(object body)
	{
		myNode.Position = new Vector2(-8321, -70233);
		//myNode.Position = Argument_1;
	}

	//level 2 spawn
	public void L2StartSpawn()
	{
		myNode.Position = new Vector2(22671, -10740);
	}

	//Level 2 Checkpoint
	public void L2Checkpoint()
	{
		myNode.Position = new Vector2(20231, 15322);
	}
}
