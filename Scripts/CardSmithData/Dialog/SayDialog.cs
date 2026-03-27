using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class SayDialog : ISay
    {
        public DialogNodeTypes DialogNodeType { get; set; } = DialogNodeTypes.Say;
        public List<IDialog> RightConnections { get; set; }
        public string Text { get; set; }

        public void Load(ref BinaryReader reader)
        {
            IDialog connection;
            Text = reader.ReadString();

            int rConnections = reader.ReadInt32();

            for (int i = 0; i < rConnections; i++)
            {
                connection = DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

                connection.Load(ref reader);

                RightConnections.Add(connection);
            }
        }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write((int)DialogNodeType);
            writer.Write(Text);
            writer.Write(RightConnections.Count);

            foreach(IDialog connection in RightConnections)
            {
                connection.Save(ref writer);
            }
        }
    }
}
