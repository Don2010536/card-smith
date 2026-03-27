using Godot;
using System;

public partial class CreateStatScreen : ColorRect
{
    [Export]
    public StatEditor StatEditor { get; set; }

	[Export]
	public LineEdit KeyText { get; set; }
	
	[Export]
	public Button CancelButton { get; set; }
    [Export]
    public Button CreateButton { get; set; }

    [Export]
    public Label ErrorLabel { get; set; }

    [Export]
    public AnimationPlayer Animator { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        CancelButton.Pressed += CancelButton_Pressed;
        CreateButton.Pressed += CreateButton_Pressed;
	}

    private void CreateButton_Pressed()
    {
        if (StatEditor.CreateStat(KeyText.Text))
        {
            HideEditor();
        }
        else
        {
            ErrorLabel.Visible = true;
        }
    }

    private void CancelButton_Pressed()
    {
        HideEditor();
    }

    private void HideEditor()
    {
        KeyText.Text = "";
        ErrorLabel.Visible = false;
        Animator.Play("HideCreateKey");
    }
}
