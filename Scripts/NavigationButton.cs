using GGC;
using Godot;
using System;

public partial class NavigationButton : Button
{
	[Export]
	public TSCN SceneToLoad { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += NavigationButton_Pressed;
	}

    public virtual void NavigationButton_Pressed()
    {
		SceneManager<TSCN>.LoadScene(SceneToLoad);
    }
}
