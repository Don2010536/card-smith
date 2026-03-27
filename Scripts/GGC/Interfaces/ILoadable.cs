using System.IO;

namespace GGC.Interfaces
{
    public interface ILoadable
    {
        public void Load(ref BinaryReader reader);
    }
}
