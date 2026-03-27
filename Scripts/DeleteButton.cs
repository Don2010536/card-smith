using Godot;
using System;

public partial class DeleteButton : Button
{
    public Action OnDelete;

    [Export]
    public Node Parent { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Pressed += DeleteButton_Pressed;
        MouseEntered += DeleteButton_Entered;
        MouseExited += DeleteButton_Exit;
	}

    private void DeleteButton_Pressed()
    {
        OnDelete?.Invoke();
        Parent?.QueueFree();
    }

    private void DeleteButton_Entered()
    {
        ((AtlasTexture)this.Icon).Region = new Rect2(new Vector2(100, 200), new Vector2(50, 50));
    }

    private void DeleteButton_Exit()
    {
        ((AtlasTexture)this.Icon).Region = new Rect2(new Vector2(100, 250), new Vector2(50, 50));
    }
}
