using System.Collections.Generic;
using System.IO;
using CardSmithData.Cards;

namespace CardSmithData.Managers
{
    public class CardManager : ISavable, ILoadable
    {
        Dictionary<int, Card> Cards = [];

        public void AddCard(Card card)
        {
            Cards[IDManager.GetID()] = card;
        }

        public void Save(ref BinaryWriter writer)
        {
            Utilities.SaveIDDict(ref writer, Cards);
        }

        public void Load(ref BinaryReader reader)
        {
            Utilities.LoadIDDict(ref reader, Cards);
        }
    }
}