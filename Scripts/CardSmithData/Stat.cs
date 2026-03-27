using GGC.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stats : ISavable, ILoadable
{
    public Dictionary<string, int> IntegerStats = [];
    public Dictionary<string, string> StringStats = [];
    public Dictionary<string, bool> BooleanStats = [];

    public void Load(ref BinaryReader reader)
    {
        int intLen = reader.ReadInt32();
        int strLen = reader.ReadInt32();
        int boolLen = reader.ReadInt32();

        for (int i = 0; i < intLen; i++)
        {
            IntegerStats.Add(reader.ReadString(), reader.ReadInt32());
        }

        for (int i = 0; i < strLen; i++)
        {
            StringStats.Add(reader.ReadString(), reader.ReadString());
        }

        for (int i = 0; i < boolLen; i++)
        {
            BooleanStats.Add(reader.ReadString(), reader.ReadBoolean());
        }
    }

    public void Save(ref BinaryWriter writer)
    {
        SyncWithBase();

        writer.Write(IntegerStats.Count);
        writer.Write(StringStats.Count);
        writer.Write(BooleanStats.Count);

        foreach (var entry in IntegerStats)
        {
            writer.Write(entry.Key);
            writer.Write(entry.Value);
        }

        foreach (var entry in StringStats)
        {
            writer.Write(entry.Key);
            writer.Write(entry.Value);
        }

        foreach (var entry in BooleanStats)
        {
            writer.Write(entry.Key);
            writer.Write(entry.Value);
        }
    }

    private void SyncWithBase()
    {
        SyncDicts(IntegerStats, DataManager.Instance.BaseStats.IntegerStats);
        SyncDicts(BooleanStats, DataManager.Instance.BaseStats.BooleanStats);
        SyncDicts(StringStats, DataManager.Instance.BaseStats.StringStats);
    }

    private void SyncDicts<T0, T1>(Dictionary<T0, T1> dict1, Dictionary<T0, T1> dict2)
    {
        foreach (var key in dict1.Keys)
        {
            if (!dict2.ContainsKey(key))
            {
                dict1.Remove(key);
            }
        }

        foreach (var key in dict2.Keys)
        {
            if (!dict1.ContainsKey(key))
            {
                dict1.Add(key, dict2[key]);
            }
        }
    }
}
