using System.IO;

namespace GGC.Interfaces
{
    public interface ISavable
    {
        public void Save(ref BinaryWriter writer);
    }
}
