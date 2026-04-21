using Godot;

public partial class Error : ColorRect
{
	public static Error Instance { get; private set; }

	[Export]
	public Label ErrorTxt { get; set; }

	[Export]
	public Button OkButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		OkButton.Pressed += OkButton_Pressed;
	}

	public void ShowError(string errorTxt)
	{
		ErrorTxt.Text = errorTxt;
		Visible = true;
	}

	private void OkButton_Pressed()
	{
		Visible = false;
	}
}
