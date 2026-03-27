using Godot;
using System;

public partial class CreateButton : Button
{
	[Export]
	public PackedScene ToSpawn { get; set; }
	[Export]
	public Node SpawnParent { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += CreateButton_Pressed;
	}

    private void CreateButton_Pressed()
    {
		SpawnParent.AddChild(ToSpawn.Instantiate());
    }
}
