using GGC;
using Godot;
using System;

public partial class MainScene : Control
{
	public static MainScene Instance { get; private set; }

	[Export]
	public Control Main { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = Vector2.Zero;

		SceneManager<TSCN>.ForgetLastScene();
		SceneManager<TSCN>.Parent = Main;

		Instance = this;
	}
}
