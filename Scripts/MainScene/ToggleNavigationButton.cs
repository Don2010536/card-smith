using Godot;
using System;

public partial class ToggleNavigationButton : Button
{
    [Export]
    public AnimationPlayer Animator { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += ToggleNavigationButton_Pressed;
	}

    private void ToggleNavigationButton_Pressed()
    {
        Visible = false;
        Animator.Play("ShowNav");
    }
}
