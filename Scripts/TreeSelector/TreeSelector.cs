using Godot;
using System.IO;

public partial class TreeSelector : Control
{
	[Export]
	public PackedScene TreeSelectButton { get; set; }

	[Export]
	public VBoxContainer SelectorContainer { get; set; }

	public override void _Ready()
	{
		TreeSelectorButton selector;

		foreach (string filename in Directory.GetFiles(Globals.DIALOGTREEP))
		{
            selector = TreeSelectButton.Instantiate <TreeSelectorButton>();
			selector.Button.TreeName = filename.Substring(0, filename.Length - 5).Split('/')[^1];

			SelectorContainer.AddChild(selector);
		}
	}
}
