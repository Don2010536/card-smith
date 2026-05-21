using CardSmithData.Cards;
using GGC.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class CardCreator : Control, ISavable, ILoadable
{
    [Export]
    public LineEdit CardName { get; set; }
    [Export]
    public SpinBox TimeToUse { get; set; }
    [Export]
    public SpinBox Uses { get; set; }
    [Export]
    public TextEdit Description { get; set; }

    public List<int> Schools { get; } = [];
    public List<int> Designators { get; } = [];
    public List<int> Keywords { get; } = [];
    public List<int> Tags { get; } = [];
    public List<EffectGroup> EffectGroups { get; } = [];

    public void Save(ref BinaryWriter writer)
    {
        
    }

    public void Load(ref BinaryReader reader)
    {
    }
}
