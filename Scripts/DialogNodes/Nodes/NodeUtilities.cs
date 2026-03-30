using CardSmithData.Dialog;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CardSmith.Scripts.DialogNodes.Nodes
{
    public class NodeUtilities
    {
        public static void FillConnections<T>(T node) where T : Node, IDialogNode
        {
            DialogGraphEdit graph = node.GetParent<DialogGraphEdit>();

            GD.Print($"[+] Finding {node.DialogNodeTypes} connection");

            IDialog connection = graph.GetConnection(node.Name);

            if (connection != null)
            {
                GD.Print($"\t[~] Connection found from {node.DialogNodeTypes} to {connection.DialogNodeType}");

                node.DialogData.RightConnections.Add(connection);
                GD.Print("[✓] Connection added");
            } 
            else
            {
                GD.Print($"[x] No connection found for {node.Name} on slot 0");
            }
        }
    }
}
