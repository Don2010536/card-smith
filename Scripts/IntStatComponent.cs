using Godot;
using System;

public partial class IntStatComponent : Control, IKeyValueEditor<int>
{
    public string Key { get; set; }
    public int OriginalValue { get; set; }
    public int Value { get; set; }

    public bool Changed => OriginalValue == Value;

    [Export]
    public bool CanBeDeleted { get; set; } = false;

    [Export]
    public Label KeyLabel { get; set; }

    [Export]
    public SpinBox ValueEdit { get; set; }

    [Export]
    public DeleteButton DeleteButton { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OriginalValue = Value;

        KeyLabel.Text = $"{Key}:";
        ValueEdit.Value = OriginalValue;

        DeleteButton.Visible = CanBeDeleted;

        ValueEdit.ValueChanged += ValueEdit_ValueChanged;
    }

    private void ValueEdit_ValueChanged(double value)
    {
        Value = Mathf.RoundToInt(value);
    }
}
