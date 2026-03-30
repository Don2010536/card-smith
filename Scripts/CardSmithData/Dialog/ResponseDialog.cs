using CardSmithData.Dialog.Responses;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class ResponseDialog : IResponse
    {
        public string RespnseText { get; set; }

        public DialogNodeTypes DialogNodeType { get; set; } = DialogNodeTypes.Response;
        public LogicalOperators LogicalOperator { get; set; }

        public List<IResponseCondition> Conditions { get; set; } = [];
        public List<IDialog> RightConnections { get; set; } = [];

        public void Load(ref BinaryReader reader)
        {
            IResponseCondition condition;
            IDialog connection;
            LogicalOperator = (LogicalOperators)reader.ReadInt32();

            int rConnections = reader.ReadInt32();
            int conditions = reader.ReadInt32();

            for (int i = 0; i < rConnections; i++)
            {
                connection = DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

                connection.Load(ref reader);

                RightConnections.Add(connection);
            }

            for (int i = 0; i < conditions; i++)
            {
                condition = (IResponseCondition)DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

                condition.Load(ref reader);

                Conditions.Add(condition);
            }
        }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write((int)DialogNodeType);
            writer.Write((int)LogicalOperator);
            writer.Write(RightConnections.Count);
            writer.Write(Conditions.Count);

            foreach (IDialog dialog in RightConnections)
            {
                dialog.Save(ref writer);
            }

            foreach (IResponseCondition condition in Conditions)
            {
                condition.Save(ref writer);
            }
        }
    }
}
