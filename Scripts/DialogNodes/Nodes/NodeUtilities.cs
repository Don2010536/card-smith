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

            LogPanel.Instance.AddMessage($"[+] Finding {node.DialogNodeTypes} connection");

            IDialog connection = graph.GetConnection(node.Name);

            if (connection != null)
            {
                LogPanel.Instance.AddDebug($"\t[~] Connection found from {node.DialogNodeTypes} to {connection.DialogNodeType}");

                node.DialogData.RightConnections.Add(connection);
                LogPanel.Instance.AddSuccess("[✓] Connection added");
            } 
            else
            {
                LogPanel.Instance.AddError($"[X] No connection found for {node.Name} on slot 0");
            }
        }
    }
}
