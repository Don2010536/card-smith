using GGC.Interfaces;

public interface ICharacter: ISavable, ILoadable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Constitution { get; set; }

    public int InitiativeModifier { get; set; }
  
    public int HandSize { get; set; }
    public int MaxHandSize { get; set; }

    public int Draws { get; set; }

    public Stats CustomStats { get; set; }
}