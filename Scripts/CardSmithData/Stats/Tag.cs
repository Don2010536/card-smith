using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tag : ITag
{
    public int ID { get; set; }
    public string Name { get; set; }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(ID);
        writer.Write(Name);
    }

    public void Load(ref BinaryReader reader)
    {
        ID = reader.ReadInt32();
        Name = reader.ReadString();
    }
}