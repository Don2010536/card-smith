using CardSmithData.Dialog;
using CardSmithData.Dialog.Responses;
using Godot;
using System;
using System.IO;

public partial class DialogTreeEditor : Control
{
	public string Filename { get; set; }

	[Export]
	public AnimationPlayer Player { get; set; }

	[Export]
	public DialogGraphEdit GraphEdit { get; set; }

	[Export]
	public Button SaveButton { get; set; }
	[Export]
	public Label FilenameLabel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Filename = "test_dialog_4";// ContentMarshal.DialogFile;

		if (Filename != "")
		{
			LoadTree();
		}

        FilenameLabel.Text = Filename;
        SaveButton.Pressed += SaveButton_Pressed;
   
        TreeExiting += DialogTreeEditor_TreeExiting;
	}

    private void DialogTreeEditor_TreeExiting()
    {
		ContentMarshal.DialogFile = "";
    }

    private void SaveButton_Pressed()
    {
		if (Filename == "")
		{
			Player.Play("ShowSave");
		}
		else
		{
			//SaveTree();
			SaveDialog();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public void SaveTree()
	{
		FilenameLabel.Text = Filename;

		if (!Directory.Exists(Globals.DIALOGTREEP))
		{
			Directory.CreateDirectory(Globals.DIALOGTREEP);
		}

		using (FileStream fs = new ($"{Globals.DIALOGTREEP}/{Filename}.tree", FileMode.OpenOrCreate))
		{
			BinaryWriter writer = new (fs);

			GraphEdit.SaveTree(ref writer);

			writer.Dispose();
		}
	}

	public void LoadTree()
    {
        using (FileStream fs = new ($"{Globals.DIALOGTREEP}/{Filename}.tree", FileMode.OpenOrCreate))
        {
            BinaryReader reader = new(fs);

            GraphEdit.LoadTree(ref reader);

			reader.Dispose();
        }
    }

	public void SaveDialog()
	{
		DialogTree tree = GraphEdit.SaveDialog();

		CheckTree(tree.EntryPoint);
	}

	public void CheckTree(IDialog dialog)
	{
        LogPanel.Instance.AddMessage($"Node type: {dialog.DialogNodeType}");
        LogPanel.Instance.AddMessage($"Connections: {dialog.RightConnections.Count}");

		foreach (IDialog connection in dialog.RightConnections)
		{
			CheckTree(connection);
		}

		switch (dialog.DialogNodeType)
		{
			case DialogNodeTypes.ShowResponse:
				foreach (IResponse response in ((ShowResponsesDialog)dialog).Responses)
				{
					CheckTree(response);
                    LogPanel.Instance.AddMessage($"Conditions: {response.Conditions.Count}");
                }
				break;
			default:
				break;
		}
	}
}
