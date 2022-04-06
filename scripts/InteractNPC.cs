using Godot;
using System;

/**
 * Handles the interaction for a friendly NPC
 * @author Lucas_C_Wright
 * @start 04-04-2022
 * @version 04-04-2022
 */
public class InteractNPC : Node {
    private bool interactable = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
        if (interactable && Input.IsActionJustPressed("Interact")) {

        } 
    }

    public void OnPlayerEnterNPCSpace(object body) {
        if (body.GetType().Name.Equals("TempPlayer")) {
            interactable = true;
        } else {
            interactable = false;
        }
    }

    public void OnPlayerExitNPCArea(object body) {

    }
    
    private void NPCInteract() {

    } 
}
