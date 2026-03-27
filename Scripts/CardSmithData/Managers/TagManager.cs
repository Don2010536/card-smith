using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TagManager<T> : ISavable, ILoadable where T : ITag, new()
{
    public Dictionary<int, T> Tags { get; private set; } = [];


    public void AddTag(T tag)
    {
        Tags[tag.ID] = tag;
    }

    public T GetTag(int id)
    {
        return Tags[id];
    }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(Tags.Count);

        foreach (int key in Tags.Keys)
        {
            writer.Write(key);
            Tags[key].Save(ref writer);
        }
    }

    public void Load(ref BinaryReader reader)
    {
        int count = reader.ReadInt32();
        int key;
        T temp;

        for (int i = 0; i < count; i++)
        {
            key = reader.ReadInt32();
            temp = new T();
            temp.Load(ref reader);

            Tags.Add(key, temp);
        }
    }
}
