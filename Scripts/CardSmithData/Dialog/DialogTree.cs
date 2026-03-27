using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class DialogTree : ISavable, ILoadable
    {
        public string Name { get; set; }

        public IDialog EntryPoint;
        
        public void Load(ref BinaryReader reader)
        {
            Name = reader.ReadString();

            EntryPoint = DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

            EntryPoint.Load(ref reader);
        }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write(Name);
            EntryPoint.Save(ref writer);
        }
    }
}
