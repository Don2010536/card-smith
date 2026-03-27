using Godot;
using System;

public partial class StringStatComponent : Control, IKeyValueEditor<string>
{
	public string Key { get; set; }
    public string OriginalValue { get; set; }
    public string Value { get; set; }

	public bool Changed => OriginalValue == Value;

	[Export]
	public bool CanBeDeleted { get; set; } = false;

    [Export]
	public Label KeyLabel { get; set; }

	[Export]
	public LineEdit ValueEdit { get; set; }

	[Export]
	public DeleteButton DeleteButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OriginalValue = Value;

		KeyLabel.Text = $"{Key}:";
		ValueEdit.Text = OriginalValue;

		DeleteButton.Visible = CanBeDeleted;

        ValueEdit.TextChanged += ValueEdit_TextChanged;
	}

    private void ValueEdit_TextChanged(string newText)
    {
		Value = newText;
    }
}
