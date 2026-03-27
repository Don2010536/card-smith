using Godot;
using System;

public partial class BoolStatComponent : Control, IKeyValueEditor<bool>
{
    public string Key { get; set; }
    public bool OriginalValue { get; set; }
    public bool Value { get; set; }

    public bool Changed => OriginalValue == Value;

    [Export]
    public bool CanBeDeleted { get; set; } = false;

    [Export]
    public Label KeyLabel { get; set; }

    [Export]
    public CheckButton ValueEdit { get; set; }

    [Export]
    public DeleteButton DeleteButton { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OriginalValue = Value;

        KeyLabel.Text = $"{Key}:";
        ValueEdit.ButtonPressed = OriginalValue;

        DeleteButton.Visible = CanBeDeleted;

        ValueEdit.Pressed += ValueEdit_Pressed;
    }

    private void ValueEdit_Pressed()
    {
        Value = ValueEdit.ButtonPressed;
    }
}
