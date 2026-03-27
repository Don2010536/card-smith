using GGC.Enums;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace GGC
{
    public class AudioProducer<T1, T2>(Node parent)
    {
        private readonly Dictionary<T1, int> overflowCounter = [];
        private readonly Node parent = parent;

        public Dictionary<T1, Dictionary<AudioStreamPlayer, MultiAudioStreamPlayer<T2>>> AudioStreamPlayers = [];

        public T1 CreateMultiStreamPlayer(T1 key, int streams, string audioBus = "Master")
        {
            overflowCounter.Add(key, 0);
            AudioStreamPlayers.Add(key, []);

            AudioStreamPlayer player;

            MultiAudioStreamPlayer<T2> multi;

            for (int i = 0; i < streams; i++)
            {
                player = new() 
                {
                    Bus = audioBus
                };
                
                multi = new(player);

                parent.AddChild(player);

                AudioStreamPlayers[key].Add(player, multi);
            }

            return key;
        }

        public void CreateStreamFromPath(T1 streamKey, T2 key, string path, float pitchScale = 1, float volumeDB = 0, AudioStreamMode mode = AudioStreamMode.Playlist)
        {
            List<AudioStream> streams = BulkResourceLoader.LoadFilesFromDir<AudioStream>(path);

            foreach (AudioStreamPlayer player in AudioStreamPlayers[streamKey].Keys)
            {
                AudioStreamPlayers[streamKey][player].Streams.Add(key, new SoundStream()
                {
                    StreamMode = mode,
                    PitchScale = pitchScale,
                    VolumeDB = volumeDB,
                    Streams = streams
                });
            }
        }

        public void CreateSingleStreamFromPath(T1 streamKey, T2 key, string path, float pitchScale = 1, float volumeDB = 0)
        {
            List<AudioStream> streams = [ResourceLoader.Load<AudioStream>(path)];

            foreach (AudioStreamPlayer player in AudioStreamPlayers[streamKey].Keys)
            {
                AudioStreamPlayers[streamKey][player].Streams.Add(key, new SoundStream()
                {
                    StreamMode = AudioStreamMode.Single,
                    PitchScale = pitchScale,
                    VolumeDB = volumeDB,
                    Streams = streams
                });
            }
        }

        public void Play(T1 streamkey, T2 sound, bool addProducers = true)
        {
            if (AudioStreamPlayers.TryGetValue(streamkey, out var players))
            {
                foreach (AudioStreamPlayer player in players.Keys)
                {
                    if (!player.Playing)
                    {
                        players[player].PlaySound(sound);
                        return;
                    }
                }

                if (addProducers)
                {
                    CopyProducer(streamkey, players, sound);
                }
            }
        }

        private void CopyProducer(T1 streamkey, Dictionary<AudioStreamPlayer, MultiAudioStreamPlayer<T2>> players, T2 sound)
        {
            overflowCounter[streamkey]++;

            GD.Print($"Adding a {streamkey} audio node. Consider increasing your initial nodes {streamkey} count by {overflowCounter[streamkey]}.");

            AudioStreamPlayer newPlayer = new()
            {
                Bus = players.Keys.First().Bus
            };

            players.Add(newPlayer, new(newPlayer));

            players[newPlayer].Streams = players[players.Keys.First()].Streams;


            parent.AddChild(newPlayer);

            players[newPlayer].PlaySound(sound);
        }
    }
}