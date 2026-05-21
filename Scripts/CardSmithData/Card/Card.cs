using System.IO;

namespace CardSmithData.Cards
{
    public class Card
    {
        public int ID { get; set; }
        public string CardName { get; set; }
        public int TimeToUse { get; set; }
        public int Uses { get; set; }
        public string Description { get; set; }

        public int[] Schools { get; set; }
        public int[] Designators { get; set; }
        public int[] Keywords { get; set; }
        public int[] Tags { get; set; }
        public EffectGroup[] EffectGroups { get; set; }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write(ID);
            writer.Write(CardName);
            writer.Write(TimeToUse);
            writer.Write(Uses);
            writer.Write(Description);

            Utilities.SaveArray(ref writer, Schools);
            Utilities.SaveArray(ref writer, Designators);
            Utilities.SaveArray(ref writer, Keywords);
            Utilities.SaveArray(ref writer, Tags);
            Utilities.SaveArray(ref writer, EffectGroups);
        }

        public void Load(ref BinaryReader reader)
        {
            
        }
    }
}