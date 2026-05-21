using System.IO;

namespace CardSmithData
{
    public interface ISavable
    {
        public void Save(ref BinaryWriter writer);
    }   
}