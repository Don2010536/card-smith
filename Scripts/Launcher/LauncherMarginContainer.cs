using GGC;
using Godot;
using System;
using System.IO;

public partial class LauncherMarginContainer : MarginContainer
{
	[Export]
	public AnimationPlayer Animator { get; set; }

	[Export]
	public Button NewProjectButton { get; set; }
	[Export]
	public Button LoadProjectButton { get; set; }

	[Export]
	public Label ErrorLabel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        NewProjectButton.Pressed += NewProjectButton_Pressed;
        LoadProjectButton.Pressed += LoadProjectButton_Pressed;
	}

    private void NewProjectButton_Pressed()
    {
        ErrorLabel.Visible = false;

        Animator.Play("ShowNewProject");
    }

    private void LoadProjectButton_Pressed()
    {
        ErrorLabel.Visible = false;
        
		FileDialog dialog = new FileDialog();
		dialog.FileMode = FileDialog.FileModeEnum.OpenDir;
		dialog.CurrentPath = Globals.ProjectPath;
		dialog.ForceNative = true;

		AddChild(dialog);

		dialog.PopupCentered();

        dialog.DirSelected += Dialog_DirSelected;
    }

    private void Dialog_DirSelected(string dir)
    {
		Globals.ProjectName = dir.Split('/')[^1];

		DataManager.Instance.ProjectDir = Globals.CombinedPath;
		DataManager.Instance.Initialize();

		SceneManager<TSCN>.LoadScene(TSCN.Main);
    }
}
