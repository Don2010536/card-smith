using System.IO;

namespace CardSmithData.Cards 
{
    public class Effect : ISavable, ILoadable
    {
        public void Load(ref BinaryReader writer)
        {
            throw new System.NotImplementedException();
        }

        public void Save(ref BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}