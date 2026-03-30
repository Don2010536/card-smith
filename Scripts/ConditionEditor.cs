using Godot;
using System;

public partial class ConditionEditor : Control
{
	public bool UnconfirmedChanges { get => ConditionName.UnconfirmedChanges; }

	public ICondition Condition { get; set; }

	[Export]
	public EditableLabeledEntry ConditionName { get; set; }
    [Export]
	public TextEdit DescriptionText { get; set; }
    [Export]
	public Label IDLabel { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		if (Condition == null) QueueFree();

		ConditionName.DeleteButton.Parent = this;
		ConditionName.Text = Condition.Name;
		ConditionName.Editor.Text = Condition.Name;
		ConditionName.Label.Text = Condition.Name;
        ConditionName.UpdateText();

		DescriptionText.Text = Condition.Description;
		IDLabel.Text = $"ID: {Condition.ID}";
	}

	/// <summary>
	/// Writes changes to condition and 
	/// </summary>
	/// <returns></returns>
	public ICondition WriteCondition()
	{
		Condition.Name = ConditionName.Text;
		Condition.Description = DescriptionText.Text;

		return Condition;
	}
}
