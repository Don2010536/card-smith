using GGC.Enums;
using Godot;
using System.Collections.Generic;

namespace GGC.Interfaces
{
    public interface IAudioStream
    {
        public int LastPlayed { get; set; }
        public float PitchScale { get; set; }
        public float VolumeDB { get; set; }

        public AudioStreamMode StreamMode { get; set; }

        public List<AudioStream> Streams { get; set; }

        public AudioStream GetLastPlayed();
        public AudioStream GetNextStream();
        public float GetVolume() 
        {
            return VolumeDB;
        }
        public float GetPitch()
        {
            return PitchScale;
        }
    }
}
