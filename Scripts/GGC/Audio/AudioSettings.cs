using GGC.Interfaces;
using Godot;
using System.IO;

namespace GGC.Audio
{
    public class AudioSettings : ISavable, ILoadable
    {
        public string BusName { get; set; } = "";
        public int BusIndex { get; set; } = -1;
        public bool ValidBus { get; set; } = false;
        public double VolumeDB { get; set; } = 100;

        public void Save(ref BinaryWriter writer)
        {
            writer.Write(BusName);
            writer.Write(BusIndex);
            writer.Write(ValidBus);
            writer.Write(VolumeDB);
        }

        public void Load(ref BinaryReader reader)
        {
            BusName = reader.ReadString();
            BusIndex = reader.ReadInt32();
            ValidBus = reader.ReadBoolean();
            VolumeDB = reader.ReadDouble();
        }
    }
}
