using Godot;
using System;

public partial class LogPanel : Panel
{
	private bool showing = false;

    private Tween show;
    private Tween hide;

	[Export]
	public RichTextLabel LogTxt { get; set; }

	public static LogPanel Instance { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ToggleLog"))
		{
			if (showing)
            {
                if (show != null) { show.Kill(); }
                if (hide != null) { hide.Kill(); }

                show = CreateTween();
                show.TweenProperty(this, "position", new Vector2(GetViewport().GetVisibleRect().Size.X, Position.Y), 0.5);
            }
			else
            {
                if (show != null) { show.Kill(); }
                if (hide != null) { hide.Kill(); }

                hide = CreateTween();
                hide.TweenProperty(this, "position", new Vector2(GetViewport().GetVisibleRect().Size.X - Size.X, Position.Y), 0.5);
            }

			showing = !showing;
		}
	}

	public void AddError(string message)
    {
        AppendText(message, DefaultColors.Red);
    }

    public void AddWarning(string message)
    {
        AppendText(message, DefaultColors.Yellow);
    }

    public void AddSuccess(string message)
    {
        AppendText(message, DefaultColors.Green);
    }

    public void AddDebug(string message)
    {
        AppendText(message, DefaultColors.Purple);
    }

    public void AddMessage(string message)
    {
        AppendText(message, DefaultColors.White);
    }

    public void AppendText(string message, DefaultColors color)
    {
        Color col = Color.Color8(255, 255, 255);

        LogTxt.PushColor(Color.FromString(Colors.ColorMap[color], col));
        LogTxt.AppendText($"{message}\n");
        LogTxt.Pop();
    }
}
