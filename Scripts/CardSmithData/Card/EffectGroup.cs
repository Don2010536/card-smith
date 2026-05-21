using System.Collections.Generic;
using System.IO;
using CardSmithData;

namespace CardSmithData.Cards
{
    public class EffectGroup : ISavable, ILoadable
    {
        public Effect[] Effects { get; } = [];

        public void Save(ref BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }

        public void Load(ref BinaryReader writer)
        {
            throw new System.NotImplementedException();
        }
    }
}