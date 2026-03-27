using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TagCreator : Control
{
    [Export]
    public PackedScene EditableTagEntry { get; set; }

    [Export]
    public VBoxContainer ConditionsVBox { get; set; }

    [Export]
    public Button CreateTagButton { get; set; }
    [Export]
    public Button SaveTagButton { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EditableTagEntry editor;

        foreach (ITag tag in DataManager.Instance.TagManager.Tags.Values)
        {
            editor = EditableTagEntry.Instantiate<EditableTagEntry>();
            editor.Tag = tag;

            ConditionsVBox.AddChild(editor);
        }

        CreateTagButton.Pressed += CreateTag;
        SaveTagButton.Pressed += Save;
    }

    public void CreateTag()
    {
        ITag tag = new Tag();
        tag.ID = IDManager.GetID();

        EditableTagEntry editor = EditableTagEntry.Instantiate<EditableTagEntry>();
        editor.Tag = tag;

        ConditionsVBox.AddChild(editor);
    }

    public void Save()
    {
        DataManager.Instance.ConditionManager.Conditions.Clear();

        IEnumerable<EditableTagEntry> conditionEditors = ConditionsVBox.GetChildren().Cast<EditableTagEntry>();

        foreach (EditableTagEntry editor in conditionEditors)
        {
            DataManager.Instance.TagManager.AddTag(editor.SaveTag() as Tag);
        }

        DataManager.Instance.SaveTags();
    }
}
