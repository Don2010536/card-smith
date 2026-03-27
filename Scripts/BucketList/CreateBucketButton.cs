using Godot;
using System;

public partial class CreateBucketButton : Button
{
	[Export]
	public AnimationPlayer Animator { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	public void OnPressed()
	{
		Animator.Play("ShowCreate");
	}
}
