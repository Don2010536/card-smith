using CardSmithData.Dialog.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class ResponseConditionDialog : IResponseCondition
    {
        public bool CustomStatUsed { get; set; }

        public DialogNodeTypes DialogNodeType { get; set; } = DialogNodeTypes.ResponseCondition;
        public ComparissonTypes ComparissonType { get; set; }
        public StatTypes StatType { get; set; }
        
        public string StatKey { get; set; }
        public string ComparrisonValue { get; set; }
        public List<IDialog> RightConnections { get; set; }

        public void Load(ref BinaryReader reader)
        {
            ComparissonType = (ComparissonTypes)reader.ReadInt32();
            StatType = (StatTypes)reader.ReadInt32();
            StatKey = reader.ReadString();
            CustomStatUsed = reader.ReadBoolean();
            ComparrisonValue = reader.ReadString();

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
            writer.Write((int)ComparissonType);
            writer.Write((int)StatType);
            writer.Write(StatKey);
            writer.Write(CustomStatUsed);
            writer.Write(ComparrisonValue);
            writer.Write(RightConnections.Count);

            foreach (IDialog dialog in RightConnections)
            {
                dialog.Save(ref writer);
            }
        }
    }
}
