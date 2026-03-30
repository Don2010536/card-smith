using CardSmith.Scripts.DialogNodes.Nodes;
using CardSmithData.Dialog;
using Godot;
using System;
using System.IO;

public partial class TriggerActionGraphNode : GraphNode, IDialogNode
{
	[Export]
	public OptionButton ActionsSelector { get; set; }

	[Export]
	public Button AddActionButton { get; set; }

	[Export]
	public HBoxContainer AddActionContainer { get; set; }
	
	[Export]
	public LineEdit ActionText { get; set; }
	[Export]
	public Button ConfirmButton { get; set; }
	[Export]
	public Button CancelButton { get; set; }
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.TriggerAction;
    public IDialog DialogData { get; set; }

    public override void _Ready()
    {
        ActionText.TextSubmitted += ActionText_TextSubmitted;

        AddActionButton.Pressed += AddActionButton_Pressed;
        ConfirmButton.Pressed += ConfirmButton_Pressed;
        CancelButton.Pressed += CancelButton_Pressed;

        InitializeActions();
	}

    private void AddActionButton_Pressed()
    {
        AddActionVisiblility(true);
    }

    private void ActionText_TextSubmitted(string newText)
    {
        AddAction();
    }

    private void ConfirmButton_Pressed()
    {
        AddAction();
    }

    private void CancelButton_Pressed()
    {
        ActionText.Text = "";

        AddActionVisiblility(false);
    }

	private void AddActionVisiblility(bool state)
    {
        ActionsSelector.Visible = !state;
        AddActionButton.Visible = !state;
        AddActionContainer.Visible = state;
    }

    private void AddAction()
    {
        DataManager.Instance.ActionManager.AddAction(ActionText.Text);
        DataManager.Instance.SaveActions();

        ActionsSelector.AddItem(ActionText.Text);

        ActionText.Text = "";

        AddActionVisiblility(false);
    }

    private void InitializeActions()
    {
        foreach (int id in DataManager.Instance.ActionManager.Actions.Keys)
        {
            ActionsSelector.AddItem(DataManager.Instance.ActionManager.Actions[id], id);
        }
    }

    public void SaveNode(ref BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(ActionsSelector.Selected);
        writer.Write(PositionOffset.X);
        writer.Write(PositionOffset.Y);
    }

    public void LoadNoad(ref BinaryReader reader)
    {
        Name = reader.ReadString();
        ActionsSelector.Select(reader.ReadInt32());
        PositionOffset = new(reader.ReadSingle(), reader.ReadSingle());
    }

    public void BuildDialog()
    {
        GD.Print("[+] Building trigger action");
        TriggerActionDialog triggerAction = new();

        foreach (var entry in DataManager.Instance.ActionManager.Actions)
        {
            if (entry.Value == ActionsSelector.Text)
            {
                triggerAction.ActionID = entry.Key;
                GD.Print($"\t[~] Action {triggerAction.ActionID}:{entry.Value}");
                break;
            }
        }

        DialogData = triggerAction;
        GD.Print("[✓] Built trigger action");
    }

    public void FillConnections()
    {
        NodeUtilities.FillConnections(this);
    }
}
