using Godot;
using System.Collections.Generic;

namespace GGC.Interfaces
{
    public interface IMultiStreamAudio<T>
    {
        public Dictionary<T, IAudioStream> Streams { get; set; }


        public void PlaySound(T key);
    }
}
