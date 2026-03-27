using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class LoadConversationGraphNode : GraphNode, IDialogNode
{
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.Load;
    public IDialog DialogData { get; set; } = new LoadDialog();

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
