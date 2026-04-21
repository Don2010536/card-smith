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
		Filename = ContentMarshal.DialogFile;

		if (Filename != "")
		{
			LoadTree();
			ContentMarshal.DialogFile = "";
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
			Saving.Instance.ShowSave();
			SaveTree();
			SaveDialog();
			Saving.Instance.HideSave();
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

		if (tree is null) return;

		tree.Name = Filename;

		DataManager.Instance.SaveDialogTree(tree);
	}
}
