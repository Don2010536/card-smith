using CardSmithData.Cards;
using CardSmithData.Managers;
using GGC.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class CardCreator : Control
{
    public bool NewCard { get; set; } = true;
    public int ID { get; set; }

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

    public void Save()
    {
        Card card = new()
        {
            CardName = CardName.Text,
            TimeToUse = (int)TimeToUse.Value,
            Uses = (int)Uses.Value,
            Description = Description.Text,
            Designators = [.. Designators],
            Schools = [.. Schools],
            Keywords = [.. Keywords],
            Tags = [.. Tags],
            EffectGroups = [.. EffectGroups]
        };

        if (NewCard)
        {
            card.ID = IDManager.GetID();
            DataManager.Instance.CardManager.AddCard(card);
        }
        else
        {
            card.ID = ID;
            DataManager.Instance.CardManager.Cards[card.ID] = card;
        }

        DataManager.Instance.SaveCards();
    }

    public void Load()
    {
        Card card = DataManager.Instance.CardManager.Cards[ID];
        CardName.Text = card.CardName;
        TimeToUse.Value = card.TimeToUse;
        Uses.Value = card.Uses;
        Description.Text = card.Description;
        Designators.AddRange(card.Designators);
        Schools.AddRange(card.Schools);
        Keywords.AddRange(card.Keywords);
        Tags.AddRange(card.Tags);
        EffectGroups.AddRange(card.EffectGroups);
    }
}
