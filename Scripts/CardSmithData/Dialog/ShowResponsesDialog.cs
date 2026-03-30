using CardSmithData.Dialog.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class ShowResponsesDialog : IShowResponses
    {
        public DialogNodeTypes DialogNodeType { get; set; } = DialogNodeTypes.ShowResponse;

        public List<IResponse> Responses { get; set; } = [];
        public List<IDialog> RightConnections { get; set; } = [];

        public void Load(ref BinaryReader reader)
        {
            IDialog connection;
            IResponse response;
            int rConnections = reader.ReadInt32();
            int responses = reader.ReadInt32();

            for (int i = 0; i < rConnections; i++)
            {
                connection = DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

                connection.Load(ref reader);

                RightConnections.Add(connection);
            }

            for (int i = 0; i  < responses; i++)
            {
                response = (IResponse)DialogFactory.BuildDialog((DialogNodeTypes)reader.ReadInt32());

                response.Load(ref reader);

                Responses.Add(response);
            }
        }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write((int)DialogNodeType);
            writer.Write(RightConnections.Count);
            writer.Write(Responses.Count);

            foreach (IDialog dialog in RightConnections)
            {
                dialog.Save(ref writer);
            }

            foreach (IResponse response in Responses)
            {
                response.Save(ref writer);
            }
        }
    }
}
