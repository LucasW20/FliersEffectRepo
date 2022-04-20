using Godot;
using System;

/**
 * Handles the interaction for a friendly NPC
 * @author Lucas_C_Wright
 * @start 04-04-2022
 * @version 04-20-2022
 */
public class InteractNPC : Node {
	private bool interactable = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		if (interactable && Input.IsActionJustPressed("interact")) {
			Console.WriteLine("Player interacted with NPC");
		} 
	}
	
	private void OnPlayerEnterNPCSpace(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) {
			interactable = true;
		}
	}

	private void OnPlayerExitNPCArea(object body) {
		if (body.GetType().Name.Equals("TempPlayer")) {
			interactable = false;
		}
	}

	private void NPCInteract() {

	} 
}