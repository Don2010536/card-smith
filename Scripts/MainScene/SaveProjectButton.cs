using Godot;
using System;

public partial class SaveProjectButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += SaveProjectButton_Pressed;
	}

    private void SaveProjectButton_Pressed()
    {
        DataManager.Instance.SaveProject();
    }
}
