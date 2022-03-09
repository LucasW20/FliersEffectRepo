using Godot;
using System;

public class TestingTimeBox : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public Vector2 BoxPos = new Vector2 { };
    public bool TimeTraveled = false;

    /**
     * Timetravel mechanic to get the player between past and future parts of the level
     * -need to disable player collider for duration of event
     */
    public void GetInput()
    {
        BoxPos = new Vector2 { };

        if (Input.IsActionJustPressed("timetravel"))
        {
            Console.WriteLine("Pressed/n");
            if(TimeTraveled == false)
            {
                BoxPos.y -= 5000;
                TimeTraveled = true;
                //Console.WriteLine("TimeT = ");
                //Console.WriteLine(TimeTraveled);
            }
            else
            {
                BoxPos.y += 5000;
                TimeTraveled = false;
                //Console.WriteLine("TimeT = ");
                //Console.WriteLine(TimeTraveled);
            }   
        }
    }

    public override void _PhysicsProcess(float delta) {
        GetInput();
        BoxPos = MoveAndSlide(BoxPos);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

