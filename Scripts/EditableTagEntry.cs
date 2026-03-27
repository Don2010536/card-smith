using Godot;
using System;

public partial class EditableTagEntry : Control
{
	public ITag Tag;

	[Export]
	public EditableLabeledEntry EditableLabeledEntry { get; set; }

	[Export]
	public Label IDLabel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Tag == null) QueueFree();

		EditableLabeledEntry.Text = Tag.Name;
		IDLabel.Text = $"ID: {Tag.ID}";
	}

	// Saves changes to tag and returns it
	public ITag SaveTag()
	{
		Tag.Name = EditableLabeledEntry.Text;
		return Tag;
	}
}
