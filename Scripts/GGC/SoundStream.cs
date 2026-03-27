using GGC.Enums;
using GGC.Interfaces;
using Godot;
using System.Collections.Generic;

namespace GGC
{
    public class SoundStream : IAudioStream
    {
        private int lastPlayed = 0;

        public int LastPlayed {
            get => lastPlayed; 
            set
            {
                lastPlayed = value == Streams.Count ? 0 : value;
            }
        }

        public float PitchScale { get; set; }
        public float VolumeDB { get; set; }

        public AudioStreamMode StreamMode { get; set; } = AudioStreamMode.Playlist;
        public List<AudioStream> Streams { get; set; } = new List<AudioStream>();

        public AudioStream GetLastPlayed()
        {
            if (Streams.Count == 0)
            {
                GD.PrintErr("There are no streams");
                return null;
            }

            return Streams[LastPlayed];
        }

        public AudioStream GetNextStream()
        {
            switch (StreamMode)
            {
                case AudioStreamMode.Playlist:
                    LastPlayed++;
                    return Streams[lastPlayed];
                case AudioStreamMode.Random:
                    return GetRandomStream();
                default:
                    return Streams[0];
            }
        }

        public float GetVolume()
        {
            return StreamMode switch
            {
                AudioStreamMode.Random => VolumeDB switch
                {
                    > 0 => (float)GD.RandRange(0, VolumeDB),
                    _ => (float)GD.RandRange(VolumeDB, 0),
                },
                _ => VolumeDB,
            };
        }

        public float GetPitch()
        {
            return StreamMode switch
            {
                AudioStreamMode.Random => PitchScale switch
                {
                    > 1 => (float)GD.RandRange(1, PitchScale),
                    _ => (float)GD.RandRange(PitchScale, 1),
                },
                _ => PitchScale,
            };
        }

        private AudioStream GetRandomStream()
        {
            int last = LastPlayed;

            LastPlayed = GD.RandRange(0, Streams.Count);

            if (last == LastPlayed)
            {
                return GetRandomStream();
            }
            else
            {
                return Streams[LastPlayed];
            }
        }
    }
}
