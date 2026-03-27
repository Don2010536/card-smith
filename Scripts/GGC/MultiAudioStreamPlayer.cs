using GGC.Interfaces;
using Godot;
using System.Collections.Generic;
using GGC.Enums;

namespace GGC
{
    public class MultiAudioStreamPlayer<T>(AudioStreamPlayer player) : IMultiStreamAudio<T>
    {
        private readonly AudioStreamPlayer player = player;

        public Dictionary<T, IAudioStream> Streams { get; set; } = [];

        public void CreateStreamFromPath(T key, string path, float pitchScale = 1, float volumeDB = 0, AudioStreamMode mode = AudioStreamMode.Playlist)
        {
            Streams.Add(key, new SoundStream()
            {
                StreamMode = mode,
                PitchScale = pitchScale,
                VolumeDB = volumeDB,
                Streams = BulkResourceLoader.LoadFilesFromDir<AudioStream>(path)
            });
        }

        public void CreateSingleStreamFromPath(T key, string path, float pitchScale = 1, float volumeDB = 0)
        {
            Streams.Add(key, new SoundStream()
            {
                StreamMode = AudioStreamMode.Single,
                PitchScale = pitchScale,
                VolumeDB = volumeDB,
                Streams = [ResourceLoader.Load<AudioStream>(path)]
            });
        }

        public void PlaySound(T key)
        {
            if (Streams.TryGetValue(key, out var stream))
            {
                player.Stream = stream.GetNextStream();
                player.PitchScale = stream.GetPitch();
                player.VolumeDb = stream.VolumeDB;
                player.Play();
            }
            else
            {
                GD.PrintErr($"There is no stream with the key {key} in {player.Name}");
            }
        }
    }
}
