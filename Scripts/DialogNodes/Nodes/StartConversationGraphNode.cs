using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class StartConversationGraphNode : GraphNode, IDialogNode
{
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.Start;
    public IDialog DialogData { get; set; } = new StartDialog();

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

    public IDialog BuildDialog()
    {
        return DialogData;
    }
}
