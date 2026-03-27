using Godot;
using System;

public partial class SaveOverlay : ColorRect
{
    [Export]
    public DialogTreeEditor Editor { get; set; }

    [Export]
    public AnimationPlayer Player { get; set; }

	[Export]
	public LineEdit SaveNameEdit { get; set; }
    [Export]
    public Button ConfirmButton { get; set; }
    [Export]
    public Button CancelButton { get; set; }
	
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        ConfirmButton.Pressed += ConfirmButton_Pressed;
        CancelButton.Pressed += CancelButton_Pressed;
	}

    private void ConfirmButton_Pressed()
    {
        Editor.Filename = SaveNameEdit.Text;

        Editor.SaveTree();
        //Editor.SaveDialog();

        SaveNameEdit.Text = "";
        Player.Play("HideSave");
    }

    private void CancelButton_Pressed()
    {
        SaveNameEdit.Text = "";
        Player.Play("HideSave");
    }
}
