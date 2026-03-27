using GGC.Interfaces;
using System;
using System.IO;

public class IDManager : ISavable, ILoadable
{
    private static int id = 0;

    public static int GetID()
    {
        id++;
        return id;
    }

    public void Load(ref BinaryReader reader)
    {
        id = reader.ReadInt32();
    }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(id);
    }
}
