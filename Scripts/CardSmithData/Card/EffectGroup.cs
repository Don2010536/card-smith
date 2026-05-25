using System.IO;

namespace CardSmithData.Cards
{
    public class EffectGroup : ISavable, ILoadable
    {
        public Effect[] Effects { get; } = [];

        public void Save(ref BinaryWriter writer)
        {
            Utilities.SaveArray(ref writer, Effects);
        }

        public void Load(ref BinaryReader reader)
        {
            Utilities.LoadArray(ref reader, Effects);
        }
    }
}