using CardSmithData.Dialog;
using CardSmithData.Dialog.Responses;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class ShowResponsesNode : GraphNode, IDialogNode
{
	[Export]
	public PackedScene ResponseEditor { get; set; }
	[Export]
	public Button AddResponseButton { get; set; }

    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.ShowResponse;
    public IDialog DialogData { get; set; }

    private int slots = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AddResponseButton.Pressed += AddResponseButton_Pressed;
	}

    private void AddResponseButton_Pressed()
    {
        ResponseVBoxContainer responseEditor = ResponseEditor.Instantiate<ResponseVBoxContainer>();

        responseEditor.TreeExited += ResponseEditor_TreeExited;

        slots++;
		AddChild(responseEditor);
        SetSlot(slots, false, 0, Color.Color8(255, 0, 0), true, 0, Color.Color8(69, 163, 102));
    }

    private void ResponseEditor_TreeExited()
    {
        slots--;
    }

    public void SaveNode(ref BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(slots);
        writer.Write(PositionOffset.X);
        writer.Write(PositionOffset.Y);

        IEnumerable<IDialogNode> responses = GetChildren().Where(x => x.GetType() != typeof(Button)).Cast<IDialogNode>();

        foreach (IDialogNode response in responses)
        {
            response.SaveNode(ref writer);
        }
    }

    public void LoadNoad(ref BinaryReader reader)
    {
        IDialogNode response;

        Name = reader.ReadString();
        slots = reader.ReadInt32();
        PositionOffset = new(reader.ReadSingle(), reader.ReadSingle());

        for (int i = 0; i < slots; i++)
        {
            response = ResponseEditor.Instantiate<IDialogNode>();
            AddChild((Node)response);
            SetSlot(i + 1, false, 0, Color.Color8(255, 0, 0), true, 0, Color.Color8(69, 163, 102));

            response.LoadNoad(ref reader);
        }
    }

    public IDialog BuildDialog()
    {
        ShowResponsesDialog showResponses = new();

        foreach (ResponseVBoxContainer response in GetChildren().Where(x => x.GetType() == typeof(ResponseVBoxContainer)))
        {
            showResponses.Responses.Add((IResponse)response.BuildDialog());
        }

        DialogData = showResponses;

        return DialogData;
    }
}
