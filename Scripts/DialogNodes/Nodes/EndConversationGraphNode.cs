using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class EndConversationGraphNode : GraphNode, IDialogNode
{
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.End;
    public IDialog DialogData { get; set; } = new EndDialog();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public IDialog BuildDialog()
    {
        EndDialog end = new ();

        return end;
    }

    public void SaveNode(ref BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(PositionOffset.X);
        writer.Write(PositionOffset.Y);
    }

    public void LoadNoad(ref BinaryReader reader)
    {
        Name = reader.ReadString();
        PositionOffset = new(reader.ReadSingle(), reader.ReadSingle());
    }
}
