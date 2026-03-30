using CardSmithData.Dialog;
using CardSmithData.Dialog.Responses;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class ResponseVBoxContainer : VBoxContainer, IDialogNode
{
    Dictionary<string, LogicalOperators> textToOperatorMap = new Dictionary<string, LogicalOperators>() {
        {"Or", LogicalOperators.Or},
        {"And", LogicalOperators.And},
    };

	[Export]
	public PackedScene ResponseConditionScene { get; set; }

	[Export]
	public LineEdit ResponseEdit { get; set; }

	[Export]
	public Button AddResponseConditionButton { get; set; }

	[Export]
	public OptionButton LogicalSelector { get; set; }

	[Export]
	public VBoxContainer ResponseConditionVBox { get; set; }
    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.Response;
    public IDialog DialogData { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        AddResponseConditionButton.Pressed += AddResponseConditionButton_Pressed;
	}

    private void AddResponseConditionButton_Pressed()
    {
		ResponseConditionControl control = ResponseConditionScene.Instantiate<ResponseConditionControl>();
        control.TreeExited += Control_TreeExited;
		
		ResponseConditionVBox.AddChild(control);
    }

    private void Control_TreeExited()
    {
		LogicalSelector.Visible = ResponseConditionVBox.GetChildCount() > 1;
    }

    public void SaveNode(ref BinaryWriter writer)
    {
        writer.Write(ResponseEdit.Text);
        writer.Write(LogicalSelector.Selected);

        IEnumerable<IDialogNode> conditions = ResponseConditionVBox.GetChildren().Cast<IDialogNode>();

        writer.Write(conditions.Count());

        foreach (IDialogNode condition in conditions)
        {
            condition.SaveNode(ref writer);
        }
    }

    public void LoadNoad(ref BinaryReader reader)
    {
        ResponseConditionControl control;

        ResponseEdit.Text = reader.ReadString();
        LogicalSelector.Select(reader.ReadInt32());

        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
        {
            control = ResponseConditionScene.Instantiate<ResponseConditionControl>();
            control.TreeExited += Control_TreeExited;

            ResponseConditionVBox.AddChild(control);

            control.LoadNoad(ref reader);
        }
    }

    public void BuildDialog()
    {
        GD.Print("\t[+] Building response");
        ResponseDialog dialog = new();
        
        dialog.RespnseText = ResponseEdit.Text;
        GD.Print($"\t\t[~] Response text {dialog.RespnseText}");

        dialog.LogicalOperator = textToOperatorMap[LogicalSelector.Text];
        GD.Print($"\t\t[~] Response condition operator {dialog.LogicalOperator}");

        foreach (ResponseConditionControl control in ResponseConditionVBox.GetChildren().Cast<ResponseConditionControl>())
        {
            control.BuildDialog();

            dialog.Conditions.Add((IResponseCondition)control.DialogData);
            GD.Print($"\t\t[~] Added response condition to response");
        }

        DialogData = dialog;
        GD.Print("\t[✓] Built response");
    }

    public void FillConnections()
    {
        GD.Print("[X] Calling FillConnections() on response directly should not be done!");
    }
}
