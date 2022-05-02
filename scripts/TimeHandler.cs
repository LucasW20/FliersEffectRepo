using Godot;
using System;

public class TimeHandler : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool hide = false;
    private int visit = 0;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void VoopSeen(Node2D body)
    {
        if (visit < 2)
        {
            this.Visible = !hide;
            hide = !hide;
            visit++;
        }
       
    }

    public void HideNPC(Node2D body)
    {
        this.Visible = false;
    }



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
