using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CharacterEditor : Control
{
	[Export]
	public PackedScene IntStatComponent { get; set; }
    [Export]
    public PackedScene StringStatComponent { get; set; }
	[Export]
    public PackedScene BoolStatComponent { get; set; }

    [Export]
    public Button SaveButton { get; set; }

	[Export]
	public LineEdit NameEdit { get; set; }

    [Export]
    public SpinBox ConstitutionSpinBox { get; set; }
	[Export]
    public SpinBox InitiativeSpinBox { get; set; }
	[Export]
    public SpinBox HandSizeSpinBox { get; set; }
	[Export] 
    public SpinBox MaxHandSizeSpinBox { get; set; }
	[Export] 
    public SpinBox DrawAmmountSpinBox { get; set; }

    [Export]
    public Label IntLabel { get; set; }
    [Export]
    public Label StringLabel { get; set; }
    [Export]
    public Label BoolLabel { get; set; }

    [Export] 
    public VBoxContainer CustomIntStatsVBox { get; set; }
	[Export]
    public VBoxContainer CustomStringStatsVBox { get; set; }
    [Export]
    public VBoxContainer CustomBoolStatsVBox { get; set; }

    [Export]
    public Label IDLabel { get; set; }

    public ICharacter Character { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Character = ContentMarshal.Character;

		if (Character == null)
		{
			Character = new Character();
			Character.Id = IDManager.GetID();
			Character.CustomStats = DataManager.Instance.BaseStats.Copy();
		}
		else
		{
			ContentMarshal.Character = null;
		}

        InitializeCharacter();

        SaveButton.Pressed += SaveButton_Pressed;
    }

	public void SaveButton_Pressed()
	{
        Character.Name = NameEdit.Text;

        Character.Constitution = Mathf.RoundToInt(ConstitutionSpinBox.Value);
        Character.InitiativeModifier = Mathf.RoundToInt(InitiativeSpinBox.Value);
        Character.HandSize = Mathf.RoundToInt(HandSizeSpinBox.Value);
        Character.MaxHandSize = Mathf.RoundToInt(MaxHandSizeSpinBox.Value);
        Character.Draws = Mathf.RoundToInt(DrawAmmountSpinBox.Value);

        IEnumerable<IntStatComponent> intStatComponents = CustomIntStatsVBox.GetChildren().Cast<IntStatComponent>();
        IEnumerable<StringStatComponent> stringStatComponents = CustomStringStatsVBox.GetChildren().Cast<StringStatComponent>();
        IEnumerable<BoolStatComponent> boolStatComponents = CustomBoolStatsVBox.GetChildren().Cast<BoolStatComponent>();

        foreach (IntStatComponent comp in intStatComponents)
        {
            Character.CustomStats.IntegerStats[comp.Key] = comp.Value;
        }

        foreach (StringStatComponent comp in stringStatComponents)
        {
            Character.CustomStats.StringStats[comp.Key] = comp.Value;
        }

        foreach (BoolStatComponent comp in boolStatComponents)
        {
            Character.CustomStats.BooleanStats[comp.Key] = comp.Value;
        }

        DataManager.Instance.CharacterManager.AddCharacter(Character, Character.Id);

        DataManager.Instance.SaveCharacters();
    }

    private void InitializeCharacter()
    {
        NameEdit.Text = Character.Name;

        ConstitutionSpinBox.Value = Character.Constitution;
        InitiativeSpinBox.Value = Character.InitiativeModifier;
        HandSizeSpinBox.Value = Character.HandSize;
        MaxHandSizeSpinBox.Value = Character.MaxHandSize;
        DrawAmmountSpinBox.Value = Character.Draws;

        IDLabel.Text = $"ID: {Character.Id}";

        InitializeCustomStats();
    }

    private void InitializeCustomStats()
    {
        IntLabel.Visible = Character.CustomStats.IntegerStats.Count > 0;
        StringLabel.Visible = Character.CustomStats.StringStats.Count > 0;
        BoolLabel.Visible = Character.CustomStats.BooleanStats.Count > 0;

        IntStatComponent intStat;
        StringStatComponent stringStat;
        BoolStatComponent boolStat;

        foreach (string key in Character.CustomStats.IntegerStats.Keys)
        {
            intStat = IntStatComponent.Instantiate<IntStatComponent>();
            intStat.Key = key;
            intStat.Value = Character.CustomStats.IntegerStats[key];

            CustomIntStatsVBox.AddChild(intStat);
        }

        foreach (string key in Character.CustomStats.StringStats.Keys)
        {
            stringStat = StringStatComponent.Instantiate<StringStatComponent>();
            stringStat.Key = key;
            stringStat.Value = Character.CustomStats.StringStats[key];

            CustomStringStatsVBox.AddChild(stringStat);
        }

        foreach (string key in Character.CustomStats.BooleanStats.Keys)
        {
            boolStat = BoolStatComponent.Instantiate<BoolStatComponent>();
            boolStat.Key = key;
            boolStat.Value = Character.CustomStats.BooleanStats[key];

            CustomBoolStatsVBox.AddChild(boolStat);
        }
    }
}
