using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Character : ICharacter
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public int Constitution { get; set; } = 7;

    public int InitiativeModifier { get; set; } = 0;

    public int HandSize { get; set; } = 5;
    public int MaxHandSize { get; set; } = 7;
    public int Draws { get; set; } = 1;

    public Stats CustomStats { get; set; }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Name);
        writer.Write(Constitution);
        writer.Write(InitiativeModifier);
        writer.Write(HandSize);
        writer.Write(MaxHandSize);
        writer.Write(Draws);

        CustomStats.Save(ref writer);
    }

    public void Load(ref BinaryReader reader)
    {
        Id = reader.ReadInt32();
        Name = reader.ReadString();
        Constitution = reader.ReadInt32();
        InitiativeModifier = reader.ReadInt32();
        HandSize = reader.ReadInt32();
        MaxHandSize = reader.ReadInt32();
        Draws = reader.ReadInt32();

        CustomStats = new Stats();
        CustomStats.Load(ref reader);
    }
}
