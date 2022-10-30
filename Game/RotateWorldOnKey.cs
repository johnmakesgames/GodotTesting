using Godot;
using System;

public class RotateWorldOnKey : RigidBody
{
	float sinceFrameVerticalRotIncrease = 0;
	float verticalRotAmount = 0;
	float sinceFrameHorizontalRotIncrease = 0;
	float horizontalRotAmount = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	horizontalRotAmount += sinceFrameHorizontalRotIncrease * delta;
	verticalRotAmount += sinceFrameVerticalRotIncrease * delta;
	RotateObjectLocal(new Vector3(0, 0, 1), horizontalRotAmount);
	RotateObjectLocal(new Vector3(1, 0, 0), verticalRotAmount);
	
	// Damping
	horizontalRotAmount -= (horizontalRotAmount/50);
	verticalRotAmount -= (verticalRotAmount/50);
	
	// Cleanup
	sinceFrameHorizontalRotIncrease = 0;
	sinceFrameVerticalRotIncrease = 0;
  }

	public override void _UnhandledInput(InputEvent @event)
	{
		const float perframeIncrease = 0.05f;
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.A)
			{
				GD.Print("Left");
				sinceFrameHorizontalRotIncrease += perframeIncrease;
			}
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.D)
			{
				GD.Print("Right");
				sinceFrameHorizontalRotIncrease -= perframeIncrease;
			}
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.W)
			{
				GD.Print("Up");
				sinceFrameVerticalRotIncrease -= perframeIncrease;
			}
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.S)
			{
				GD.Print("Down");
				sinceFrameVerticalRotIncrease += perframeIncrease;
			}
			if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
			{
				GetTree().Quit();
			}
		}
	}
}
