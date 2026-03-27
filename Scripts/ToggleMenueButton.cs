using Godot;
using System;

public partial class ToggleMenueButton : Button
{
	[Export]
	public CanvasItem ToToggle { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        MouseEntered += ToggleMenueButton_MouseEntered;
        MouseExited += ToggleMenueButton_MouseExited;
        Pressed += ToggleMenueButton_Pressed;
	}

    private void ToggleMenueButton_MouseExited()
    {
        SetIcon(ToToggle.Visible);
        
    }

    private void ToggleMenueButton_MouseEntered()
    {
        SetIcon(!ToToggle.Visible);
    }

    private void ToggleMenueButton_Pressed()
    {
		ToToggle.Visible = !ToToggle.Visible;
    }

    private void SetIcon(bool state)
    {
        if (state)
        {
            ((AtlasTexture)Icon).Region = new Rect2(new Vector2(400, 300), new Vector2(50, 50));
        }
        else
        {
            ((AtlasTexture)Icon).Region = new Rect2(new Vector2(100, 50), new Vector2(50, 50));
        }
    }
}
