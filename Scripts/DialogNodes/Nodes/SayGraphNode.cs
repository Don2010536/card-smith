using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class SayGraphNode : GraphNode, IDialogNode
{
    private DialogGraphEdit graph;

	[Export]
	public TextEdit ToSayEdit { get; set; }

    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.Say;
    public IDialog DialogData { get; set; } = new SayDialog();

    public override void _Ready()
	{
        graph = GetParent<DialogGraphEdit>();
	}
    
    public void SaveNode(ref BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(ToSayEdit.Text);
        writer.Write(PositionOffset.X);
        writer.Write(PositionOffset.Y);
    }

    public void LoadNoad(ref BinaryReader reader)
    {
        Name = reader.ReadString();
        ToSayEdit.Text = reader.ReadString();
        PositionOffset = new(reader.ReadSingle(), reader.ReadSingle());
    }

    public IDialog BuildDialog()
    {
        ((SayDialog)DialogData).Text = ToSayEdit.Text;

        return DialogData;
    }
}
