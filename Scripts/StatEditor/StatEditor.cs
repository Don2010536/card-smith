using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Godot.WebSocketPeer;

public partial class StatEditor : Control
{
    enum Box
    {
        None,
        Int,
        String,
        Bool
    }

    private Box box = Box.None;
    private bool canBeDeleted = true;

    [Export]
    public PackedScene IntStatComponent { get; set; }
    [Export]
    public PackedScene StringStatComponent { get; set; }
    [Export]
    public PackedScene BoolStatComponent { get; set; }

    [Export]
    public Button AddIntegerStatButton { get; set; }
    [Export]
    public Button AddStringStatButton { get; set; }
    [Export]
    public Button AddBoolStatButton { get; set; }

    [Export]
    public VBoxContainer IntStatVBox { get; set; }
    [Export]
    public VBoxContainer StringStatVBox { get; set; }
    [Export]
    public VBoxContainer BoolStatVBox { get; set; }

    [Export]
    public Button SaveButton { get; set; }

    [Export]
    public AnimationPlayer Animator { get; set; }

    public override void _Ready()
    {
        LoadDict<IntStatComponent, int>(IntStatComponent, IntStatVBox, DataManager.Instance.BaseStats.IntegerStats);
        LoadDict<StringStatComponent, string>(StringStatComponent, StringStatVBox, DataManager.Instance.BaseStats.StringStats);
        LoadDict<BoolStatComponent, bool>(BoolStatComponent, BoolStatVBox, DataManager.Instance.BaseStats.BooleanStats);

        AddIntegerStatButton.Pressed += AddIntegerStatButton_Pressed;
        AddStringStatButton.Pressed += AddStringStatButton_Pressed;
        AddBoolStatButton.Pressed += AddBoolStatButton_Pressed;

        SaveButton.Pressed += SaveButton_Pressed;
    }

    public bool CreateStat(string key)
    {
        return box switch
        {
            Box.String => AddStatIfValid<StringStatComponent, string>(StringStatComponent, StringStatVBox, key, ""),
            Box.Bool => AddStatIfValid<BoolStatComponent, bool>(BoolStatComponent, BoolStatVBox, key, false),
            Box.Int => AddStatIfValid<IntStatComponent, int>(IntStatComponent, IntStatVBox, key, 0),
            _ => false
        };
    }

    private bool AddStatIfValid<T0, T1>(PackedScene scene, VBoxContainer container, string key, T1 value) where T0 : Node, IKeyValueEditor<T1>
    {
        if (CheckKeys<T0, T1>(container, key))
        {
            box = Box.None;

            container.AddChild(InstantiateKeyValueEditor<T0, T1>(scene, key, value));

            return true;
        }
        return false;
    } 

    private bool CheckKeys<T0, T1>(VBoxContainer container, string key) where T0 : IKeyValueEditor<T1>
    {
        IEnumerable<T0> statComponents = container.GetChildren().Cast<T0>();

        foreach (T0 statComponent in statComponents)
        {
            if (statComponent.Key == key)
            {
                return false;
            }
        }

        return true;
    }

    private void AddIntegerStatButton_Pressed()
    {
        box = Box.Int;
        Animator.Play("ShowCreateKey");
    }

    private void AddStringStatButton_Pressed()
    {
        box = Box.String;
        Animator.Play("ShowCreateKey");
    }

    private void AddBoolStatButton_Pressed()
    {
        box = Box.Bool;
        Animator.Play("ShowCreateKey");
    }

    private void SaveButton_Pressed()
    {
        SyncDict<IntStatComponent, int>(IntStatVBox, DataManager.Instance.BaseStats.IntegerStats);
        SyncDict<StringStatComponent, string>(StringStatVBox, DataManager.Instance.BaseStats.StringStats);
        SyncDict<BoolStatComponent, bool>(BoolStatVBox, DataManager.Instance.BaseStats.BooleanStats);

        DataManager.Instance.SaveBaseStats();
    }

    private void SyncDict<T0, T1>(VBoxContainer container, Dictionary<string, T1> dict) where T0 : IKeyValueEditor<T1>
    {
        dict.Clear();

        IEnumerable<T0> statComponents = container.GetChildren().Cast<T0>();

        foreach (T0 statComponent in statComponents)
        {
            dict[statComponent.Key] = statComponent.Value;
        }
    }


    private void LoadDict<T0, T1>(PackedScene scene, VBoxContainer container, Dictionary<string, T1> dict) where T0 : Node, IKeyValueEditor<T1>
    {
        foreach (string key in dict.Keys)
        {
            container.AddChild(InstantiateKeyValueEditor<T0, T1>(scene, key, dict[key]));
        }
    }

    private T0 InstantiateKeyValueEditor<T0, T1>(PackedScene scene, string key, T1 value) where T0 : Node, IKeyValueEditor<T1>
    {
        T0 editor = scene.Instantiate<T0>();

        editor.Key = key;
        editor.Value = value;
        editor.CanBeDeleted = canBeDeleted;

        return editor;
    }
}
