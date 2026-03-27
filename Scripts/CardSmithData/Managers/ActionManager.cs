using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActionManager : ISavable, ILoadable
{
    public Dictionary<int, string> Actions { get; private set; } = [];

    public void AddAction(string action)
    {
        Actions[IDManager.GetID()] = action;
    }

    public string GetAction(int id)
    {
        return Actions[id];
    }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(Actions.Count);

        foreach (int key in Actions.Keys)
        {
            writer.Write(key);
            writer.Write(Actions[key]);
        }
    }

    public void Load(ref BinaryReader reader)
    {
        int count = reader.ReadInt32();
        int key;

        for (int i = 0; i < count; i++)
        {
            key = reader.ReadInt32();
            Actions.Add(key, reader.ReadString());
        }
    }
}
