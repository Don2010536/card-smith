using Godot;
using System;

public partial class CharacterSelector : Control
{
	[Export]
	public PackedScene CharacterSelectButton { get; set; }

	[Export]
	public VBoxContainer SelectorContainer { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DeletableCharacterSelector selector;

		foreach (int key in DataManager.Instance.CharacterManager.Characters.Keys)
		{
            selector = CharacterSelectButton.Instantiate <DeletableCharacterSelector>();
            selector.Button.ID = key;
            selector.Button.CharacterName = DataManager.Instance.CharacterManager.GetCharacter(key).Name;

			SelectorContainer.AddChild(selector);
		}
	}
}
