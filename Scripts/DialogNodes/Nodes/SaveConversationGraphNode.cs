using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class SaveConversationGraphNode : GraphNode, IDialogNode
{
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.Save;
    public IDialog DialogData { get; set; }

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
        DialogData = new SaveDialog();

        return DialogData;
    }
}
