using Godot;
using System;
using System.IO;

public partial class TreeButton : NavigationButton
{
	public string TreeName { get; set; }

	[Export]
	public DeleteButton DeleteButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SceneToLoad = TSCN.DialogTreeEditor;
		Text = TreeName;

		Pressed += NavigationButton_Pressed;

		DeleteButton.OnDelete = new Action(() =>
		{
			File.Delete($"{Globals.DIALOGTREEP}/{TreeName}.tree");
			File.Delete($"{Globals.DIALOGTREE_DATAP}/{TreeName}.diag");
		});
	}

    public override void NavigationButton_Pressed()
    {
		ContentMarshal.DialogFile = TreeName;

        base.NavigationButton_Pressed();
    }
}
