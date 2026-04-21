using Godot;

public partial class Saving : ColorRect
{
	
	public static Saving Instance { get; private set; }

	[Export]
	public AnimationPlayer SaveTextAnimator { get; set; }
	[Export]
	public AnimationPlayer SaveAnimator { get; set; }

    public override void _Ready()
    {
		Instance = this;
    }

	public void ShowSave()
	{
		SaveTextAnimator.Play("Saving");
		SaveAnimator.Play("ShowSave");
	}

	public void HideSave()
	{
		SaveTextAnimator.Stop();
		SaveAnimator.Queue("HideSave");
	}

	public void HideSaveNow()
	{
		SaveTextAnimator.Stop();
		SaveAnimator.Play("HideSave");
	}
}
