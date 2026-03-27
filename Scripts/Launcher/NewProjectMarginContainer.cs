using GGC;
using Godot;
using System;
using System.IO;

public partial class NewProjectMarginContainer : MarginContainer
{
	[Export]
	public AnimationPlayer Animator { get; set; }

	[Export]
	public Label ErrorLabel { get; set; }

	[Export]
	public LineEdit ProjectName { get; set; }

	[Export]
	public Button SaveButton { get; set; }
	[Export]
	public Button BackButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SaveButton.Pressed += SaveButton_Pressed;
        BackButton.Pressed += BackButton_Pressed;
	}

    private void BackButton_Pressed()
    {
		Animator.Play("HideNewProject");

		ErrorLabel.Visible = false;
		ProjectName.Text = "";
    }

    private void SaveButton_Pressed()
    {
		if (Directory.Exists($"{Globals.ProjectPath}/{ProjectName.Text.ToUpper()}"))
		{
			ErrorLabel.Visible = true;
		} else
		{
            Globals.ProjectName = ProjectName.Text.ToUpper();

			Directory.CreateDirectory(Globals.CombinedPath);

			DataManager.Instance.ProjectDir = Globals.CombinedPath;
			DataManager.Instance.Initialize();

			SceneManager<TSCN>.LoadScene(TSCN.Main);
        }
    }
}
