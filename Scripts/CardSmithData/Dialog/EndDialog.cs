using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class EndDialog : IEnd
    {
        public DialogNodeTypes DialogNodeType { get; set; } = DialogNodeTypes.End;
        public List<IDialog> RightConnections { get; set; } = [];

        public void Load(ref BinaryReader reader)
        {
            IDialog connection;
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
            writer.Write(RightConnections.Count);

            foreach (IDialog dialog in RightConnections)
            {
                dialog.Save(ref writer);
            }
        }
    }
}
