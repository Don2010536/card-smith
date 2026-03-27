using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ConditionsCreator : Control
{
	[Export]
	public PackedScene ConditionEditor { get; set; }

	[Export]
	public VBoxContainer ConditionsVBox { get; set; }

	[Export]
	public Button CreateConditionButton { get; set; }
    [Export]
    public Button SaveConditionButton { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		ConditionEditor editor;

		foreach (ICondition condition in DataManager.Instance.ConditionManager.Conditions.Values)
		{
			editor = ConditionEditor.Instantiate<ConditionEditor>();
			editor.Condition = condition;

			ConditionsVBox.AddChild(editor);
		}

		CreateConditionButton.Pressed += CreateCondition;
		SaveConditionButton.Pressed += Save;
	}

	public void CreateCondition()
	{
		ICondition condition = new Condition();
		condition.ID = IDManager.GetID();

        ConditionEditor editor = ConditionEditor.Instantiate<ConditionEditor>();
        editor.Condition = condition;

        ConditionsVBox.AddChild(editor);
    }

	public void Save()
	{
		DataManager.Instance.ConditionManager.Conditions.Clear();

		IEnumerable<ConditionEditor> conditionEditors = ConditionsVBox.GetChildren().Cast<ConditionEditor>();

		foreach (ConditionEditor editor in conditionEditors)
		{
			DataManager.Instance.ConditionManager.AddCondition(editor.WriteCondition() as Condition);
		}

		DataManager.Instance.SaveConditions();
	}
}
