using CardSmith.Data;
using Godot;
using System;
using System.Collections.Generic;

public partial class CreateBucketScreen : Control
{
	[Export]
	public LineEdit NameLine { get; set; }

    [Export]
    public Button CancelButton { get; set; }
    [Export]
    public Button CreateButton { get; set; }

    public EventHandler<Bucket<List<string>, string>> BucketCreated;
	public EventHandler CancelPressed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CancelButton.Pressed += OnCancelPressed;
		CreateButton.Pressed += OnCreatePressed;
	}

	public void Focus()
	{
		NameLine.GrabFocus();
	}

	public void OnCreatePressed()
	{
		BucketCreated?.Invoke(this, new Bucket<List<string>, string>(NameLine.Text, []));
		NameLine.Text = "";
	}

	public void OnCancelPressed()
	{
		CancelPressed?.Invoke(this, EventArgs.Empty);
        NameLine.Text = "";
    }
}
