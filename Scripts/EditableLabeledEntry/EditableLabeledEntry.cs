using Godot;
using System;

public partial class EditableLabeledEntry : Control
{
	public string Text { get; set; }

	public bool UnconfirmedChanges { get; set; }
	public bool Edited { get; set; }

    [Export]
    public MarginContainer EditorContainer { get; set; }
    [Export]
	public LineEdit Editor { get; set; }
	[Export]
	public Label Label { get; set; }

	[Export]
	public Button ConfirmButton { get; set; }
    [Export]
    public Button EditButton { get; set; }
    [Export]
    public DeleteButton DeleteButton { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Editor.Text = Text;
		Label.Text = Text == "" ? "Click the pencil to edit" : Text;

		ConfirmButton.Pressed += Confirm;
		EditButton.Pressed += ShowEdit;
	}

	public void ShowEdit()
	{
        UnconfirmedChanges = true;

        EditorContainer.Visible = true;
        ConfirmButton.Visible = true;
		Label.Visible = false;
		EditButton.Visible = false;
		DeleteButton.Visible = false;
	}

	public void Confirm()
    {
		UpdateText();

		UnconfirmedChanges = false;

        EditorContainer.Visible = false;
        ConfirmButton.Visible = false;
        Label.Visible = true;
        EditButton.Visible = true;
        DeleteButton.Visible = true;
    }

	public void UpdateText()
	{
        Edited = Text != Editor.Text || Label.Text != Text;

        if (Edited)
        {
            Text = Editor.Text;
            Label.Text = Text == "" ? "Click the pencil to edit" : Text;
        }
    }
}
