using Godot;
using System;

public partial class SpawnDialogNodeButton : Button
{
    [Export]
    PackedScene DialogNodeScene { get; set; }
    [Export]
    public DialogGraphEdit Graph { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += SpawnDialogNodeButton_Pressed;
	}

    private void SpawnDialogNodeButton_Pressed()
    {
        GraphNode node = DialogNodeScene.Instantiate<GraphNode>();

        Graph.AddChild(node);

        node.PositionOffset = ((Graph.PopUpPos + Graph.ScrollOffset) / Graph.Zoom);
    }
}
