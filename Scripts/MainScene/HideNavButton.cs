using Godot;
using System;

public partial class HideNavButton : Button
{
	[Export]
	public AnimationPlayer Animator { get; set; }

	[Export]
	public Button ToggleNavButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += HideNavButton_Pressed;
	}

    private void HideNavButton_Pressed()
    {
		Animator.Play("HideNav");
		ToggleNavButton.Visible = true;
    }
}
