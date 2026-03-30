using CardSmithData.Dialog;
using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

public partial class ResponseConditionControl : MarginContainer, IDialogNode
{
	private struct StatType
	{
		internal StatTypes statType;
		internal bool custom;
	}

	Dictionary<string, StatType> statMap = [];

	Dictionary<string, ComparissonTypes> comparrisonMap = new() {
		{ "greater than", ComparissonTypes.GreaterThan },
		{ "greater than or equal to", ComparissonTypes.GreaterThanOrEqual },
		{ "less than", ComparissonTypes.LessThan },
		{ "less than or equal to", ComparissonTypes.LessThanOrEqual },
		{ "equal to", ComparissonTypes.Equal },
	};

	[Export]
	public OptionButton StatSelector { get; set; }
	[Export]
	public OptionButton ComparrisonSelector { get; set; }
	[Export]
	public OptionButton TrueFalseSelector { get; set; }

	[Export]
	public LineEdit StringCompareValue { get; set; }

	[Export]
	public SpinBox IntCompareValue { get; set; }

    public DialogNodeTypes DialogNodeTypes { get; set; } = DialogNodeTypes.ResponseCondition;

    public IDialog DialogData { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		InitializeStatMap();
        InitializeStatSelector();

        InitializeComparrisonSelector();

        StatSelector.ItemSelected += StatSelector_ItemSelected;

		StatSelector.Select(0);

        string key = StatSelector.GetItemText((int)0);

        UpdateComparrisonSelector(key);
        UpdateComparrisonValue(key);
    }

    private void InitializeComparrisonSelector()
    {
        foreach (string key in comparrisonMap.Keys)
        {
            ComparrisonSelector.AddItem(key);
        }
    }

    private void InitializeStatSelector()
    {
		foreach (string key in statMap.Keys)
		{
			StatSelector.AddItem(key);
		}
    }

    private void StatSelector_ItemSelected(long index)
    {
		string key = StatSelector.GetItemText((int)index);

        UpdateComparrisonSelector(key);
		UpdateComparrisonValue(key);
    }

	private void UpdateComparrisonValue(string key)
	{
		switch (statMap[key].statType)
		{
			case StatTypes.Int:
                StringCompareValue.Hide();
                TrueFalseSelector.Hide();
                IntCompareValue.Show();
                break;
            case StatTypes.String:
                StringCompareValue.Show();
                TrueFalseSelector.Hide();
                IntCompareValue.Hide();
                break;
			case StatTypes.Bool:
                StringCompareValue.Hide();
                TrueFalseSelector.Show();
				IntCompareValue.Hide();
				break;
		}
	}

    private void UpdateComparrisonSelector(string key)
    {
		if (statMap[key].statType == StatTypes.Int)
		{
            SetComparrisonDisabled(false);
		}
		else
		{
			SetComparrisonDisabled(true);
		}
    }

    private void SetComparrisonDisabled(bool disabled)
    {
        ComparrisonSelector.SetItemDisabled(0, disabled);
        ComparrisonSelector.SetItemDisabled(1, disabled);
        ComparrisonSelector.SetItemDisabled(2, disabled);
        ComparrisonSelector.SetItemDisabled(3, disabled);

		if (disabled)
		{
			ComparrisonSelector.Select(4);
		}
    }

    private void InitializeStatMap()
	{
		StatType intStatType = new()
		{
			statType = StatTypes.Int,
			custom = false
		};

		statMap.Add("Constitution", intStatType);
		statMap.Add("Initiative Modifier", intStatType);
		statMap.Add("Max Hand Size", intStatType);

		MapDictToStatMap(DataManager.Instance.BaseStats.IntegerStats, StatTypes.Int);
		MapDictToStatMap(DataManager.Instance.BaseStats.StringStats, StatTypes.String);
		MapDictToStatMap(DataManager.Instance.BaseStats.BooleanStats, StatTypes.Bool);
    }

	private void MapDictToStatMap<T>(Dictionary<string, T> dict, StatTypes statType)
    {
        StatType customType = new()
        {
            statType = statType,
            custom = true
        };

        foreach (string key in dict.Keys)
        {
            statMap.Add(key, customType);
        }
    }

    public IDialog Save(ref BinaryWriter writer)
    {
        throw new NotImplementedException();
    }

    public void SaveNode(ref BinaryWriter writer)
    {
		writer.Write(StatSelector.Selected);
		writer.Write(ComparrisonSelector.Selected);
		writer.Write(TrueFalseSelector.Selected);
		writer.Write(StringCompareValue.Text);
		writer.Write(IntCompareValue.Value);
    }

    public void LoadNoad(ref BinaryReader reader)
    {
		StatSelector.Select(reader.ReadInt32());
        ComparrisonSelector.Select(reader.ReadInt32());
        TrueFalseSelector.Select(reader.ReadInt32());
        StringCompareValue.Text = reader.ReadString();
		IntCompareValue.Value = reader.ReadDouble();
    }

    public void BuildDialog()
    {
        LogPanel.Instance.AddMessage("\t\t[+] Building response condition");
        ResponseConditionDialog responseCondition = new ResponseConditionDialog();
        string key = StatSelector.GetItemText(StatSelector.Selected);

        responseCondition.StatKey = key;
        LogPanel.Instance.AddDebug($"\t\t\t[~] Comparison stat key: {responseCondition.StatKey}");

        responseCondition.StatType = statMap[key].statType;
        LogPanel.Instance.AddDebug($"\t\t\t[~] Comparison stat type: {responseCondition.StatType}");

        responseCondition.ComparissonType = comparrisonMap[ComparrisonSelector.GetItemText(ComparrisonSelector.Selected)];
        LogPanel.Instance.AddDebug($"\t\t\t[~] Comparison type: {responseCondition.ComparissonType}");

        switch (responseCondition.StatType)
        {
            case StatTypes.Int:
                responseCondition.ComparrisonValue = IntCompareValue.Value.ToString();
                break;
            case StatTypes.String:
                responseCondition.ComparrisonValue = StringCompareValue.Text;
                break;
            case StatTypes.Bool:
                responseCondition.ComparrisonValue = TrueFalseSelector.ButtonPressed.ToString();
                break;
        }
        LogPanel.Instance.AddDebug($"\t\t\t[~] Comparison value: {responseCondition.ComparrisonValue}");

        responseCondition.CustomStatUsed = statMap[key].custom;
        LogPanel.Instance.AddDebug($"\t\t\t[~] Custom stat used: {responseCondition.CustomStatUsed}");

        DialogData = responseCondition;
        LogPanel.Instance.AddSuccess($"\t\t[✓] Built response condition");
    }

    public void FillConnections()
    {
        LogPanel.Instance.AddSuccess("[✓] Response condition does not have connections");
    }
}
