using System.IO;

namespace CardSmithData
{
    public interface ILoadable
    {
        public void Load(ref BinaryReader writer);
    }   
}