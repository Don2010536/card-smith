using GGC;
using Godot;
using System;

public partial class Gamemaster : Node2D
{
    private Scenes scenes;
    
	public static Gamemaster Instance { get; private set; }

    [Export]
    public CanvasLayer CanvasLayer { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
    
        scenes = new Scenes();
        SceneManager<TSCN>.ScenePath = Globals.SCENESP;
        SceneManager<TSCN>.SceneMap = scenes;
        SceneManager<TSCN>.Parent = CanvasLayer;

        SceneManager<TSCN>.LoadScene(TSCN.Launcher);
    }
}
