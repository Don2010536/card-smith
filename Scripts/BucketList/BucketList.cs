using CardSmith.Data;
using Godot;
using System;
using System.Collections.Generic;

public partial class BucketList : Control
{
	[Export]
	public PackedScene EditableLine { get; set; }
	
	[Export]
	public CreateBucketScreen CreateBucketScreen { get; set; }
	[Export]
	public AnimationPlayer Animator { get; set; }
	[Export]
	public VBoxContainer Buckets { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateBucketScreen.CancelPressed += CancelPressed;
		CreateBucketScreen.BucketCreated += BucketCreated;
	}

	public void BucketCreated(object sender, Bucket<List<string>, string> bucket)
	{
		EditableLabeledEntry labeledEntry = EditableLine.Instantiate<EditableLabeledEntry>();
		labeledEntry.Text = bucket.Name;

		Buckets.AddChild(labeledEntry);

        Animator.Play("HideCreate");
    }

	public void CancelPressed(object sender, EventArgs e)
	{
		Animator.Play("HideCreate");
	}
}
