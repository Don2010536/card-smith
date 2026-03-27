using Godot;
using System;

public partial class CharacterSelectButton : NavigationButton
{
	public int ID { get; set; }
	public string CharacterName { get; set; }

	[Export]
	public DeleteButton DeleteButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SceneToLoad = TSCN.CharacterCreator;
		Text = $"{ID} : {CharacterName}";

		Pressed += NavigationButton_Pressed;

		DeleteButton.OnDelete = new Action(() =>
		{
			DataManager.Instance.CharacterManager.Characters.Remove(ID);
			DataManager.Instance.SaveCharacters();
		});
	}

    public override void NavigationButton_Pressed()
    {
		ContentMarshal.Character = DataManager.Instance.CharacterManager.GetCharacter(ID);

        base.NavigationButton_Pressed();
    }
}
