using CardSmithData.Dialog;
using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class DialogGraphEdit : GraphEdit
{
    public Vector2 PopUpPos { get; set; }

    [Export]
    public MarginContainer CreateNodePopup { get; set; }

    [Export]
    public Godot.Collections.Dictionary<DialogNodeTypes, PackedScene> ToEmptyMap { get; set; }
    [Export]
    public Godot.Collections.Dictionary<DialogNodeTypes, PackedScene> LoadMap { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ConnectionRequest += DialogGraphEdit_ConnectionRequest;
        AddValidConnectionType(0, 0);

        DisconnectionRequest += DialogGraphEdit_DisconnectionRequest;

        PopupRequest += DialogGraphEdit_PopupRequest;
        DeleteNodesRequest += DialogGraphEdit_DeleteNodesRequest;
        ConnectionToEmpty += DialogGraphEdit_ConnectionToEmpty;
    }

    private void DialogGraphEdit_ConnectionToEmpty(StringName fromNode, long fromPort, Vector2 releasePosition)
    {
        PackedScene scene;

        if (ToEmptyMap.TryGetValue(GetNode<IDialogNode>(fromNode.ToString()).DialogNodeTypes, out scene))
        {
            GraphElement node = scene.Instantiate<GraphElement>();
            node.PositionOffset = ((releasePosition + ScrollOffset) / Zoom);
            AddChild(node);
            ConnectNode(fromNode, (int)fromPort, node.Name, 0);
        }
    }

    private void DialogGraphEdit_DeleteNodesRequest(Godot.Collections.Array<StringName> nodes)
    {
        foreach (var node in nodes)
        {
            GetNode(node.ToString()).QueueFree();
        }
    }

    private void DialogGraphEdit_PopupRequest(Vector2 atPosition)
    {
        PopUpPos = atPosition;
        CreateNodePopup.Position = atPosition;
        CreateNodePopup.Visible = true;
    }

    private void DialogGraphEdit_DisconnectionRequest(StringName fromNode, long fromPort, StringName toNode, long toPort)
    {
        DisconnectNode(fromNode, (int)fromPort, toNode, (int)toPort);
    }

    private void DialogGraphEdit_ConnectionRequest(StringName fromNode, long fromPort, StringName toNode, long toPort)
    {
        ConnectNode(fromNode, (int)fromPort, toNode, (int)toPort);
    }

    public void SaveTree(ref BinaryWriter writer)
    {
        IEnumerable<IDialogNode> nodes = GetChildren().Where(x => x is GraphNode).Cast<IDialogNode>();

        writer.Write(nodes.Count());

        foreach (IDialogNode node in nodes)
        {
            writer.Write((int)node.DialogNodeTypes);
            node.SaveNode(ref writer);
        }

        writer.Write(Zoom);
        writer.Write(ScrollOffset.X);
        writer.Write(ScrollOffset.Y);

        writer.Write(Connections.Count());


        for (int i = 0; i < Connections.Count; i++)
        {
            writer.Write(Connections[i]["from_node"].ToString());
            writer.Write(Connections[i]["from_port"].AsInt32());
            writer.Write(Connections[i]["to_node"].ToString());
            writer.Write(Connections[i]["to_port"].AsInt32());
            writer.Write(Connections[i]["keep_alive"].AsBool());
        }
    }

    public void LoadTree(ref BinaryReader reader)
    {
        DialogNodeTypes nodeType;
        GraphElement node;

        int count = reader.ReadInt32();


        for (int i = 0; i < count; i++)
        {
            nodeType = (DialogNodeTypes)reader.ReadInt32();
            node = LoadMap[nodeType].Instantiate<GraphElement>();

            AddChild(node);

            ((IDialogNode)node).LoadNoad(ref reader);
        }

        Zoom = reader.ReadSingle();
        float offsetX = reader.ReadSingle();
        float offsetY = reader.ReadSingle();

        ScrollOffset = new (offsetX, offsetY);

        string fromNode;
        int fromPort;
        string toNode;
        int toPort;
        bool keepAlive;

        count = reader.ReadInt32();

        for (int i = 0; i < count; ++i)
        {
            fromNode = reader.ReadString();
            fromPort = reader.ReadInt32();
            toNode = reader.ReadString();
            toPort = reader.ReadInt32();
            keepAlive = reader.ReadBoolean();

            ConnectNode(fromNode, fromPort, toNode, toPort, keepAlive);
        }
    }

    public DialogTree SaveDialog()
    {
        DialogTree tree = new ();

        if (!ValidateTree())
        {
            return null;
        }

        Dictionary<string, IDialogNode> nodeMap = [];
        IDialogNode node = null;
        string from;
        string to;

        for (int i = 0; i < Connections.Count; i++)
        {
            from = Connections[i]["from_node"].AsString();
            to = Connections[i]["to_node"].AsString();

            if (!nodeMap.ContainsKey(from))
            {
                node = GetNode<IDialogNode>(Connections[i]["from_node"].ToString());
                node.BuildDialog();

                nodeMap.Add(from, node);
            }

            if (!nodeMap.ContainsKey(to))
            {
                node = GetNode<IDialogNode>(Connections[i]["to_node"].ToString());
                node.BuildDialog();

                nodeMap.Add(to, node);
            }
        }

        foreach (string key in nodeMap.Keys)
        {
            nodeMap[key].FillConnections();

            if (nodeMap[key].DialogNodeTypes == DialogNodeTypes.Start)
            {
                tree.EntryPoint = nodeMap[key].DialogData;
            }
        }

        return tree;
    }

    public IDialog GetConnection(string from_node, int fromPort = -1)
    {
        for (int i = 0; i < Connections.Count; i++)
        {
            if (from_node == Connections[i]["from_node"].ToString() && (fromPort == Connections[i]["from_port"].AsInt32() || fromPort == -1))
            {
                return GetNode<IDialogNode>(Connections[i]["to_node"].ToString()).DialogData;
            }
        }

        return null;
    }

    private bool ValidateTree()
    {
        bool state = false;

        IEnumerable<IDialogNode> nodes = GetChildren().Where(x => x is GraphNode).Cast<IDialogNode>();

        foreach (IDialogNode node in nodes)
        {
            if (node.DialogNodeTypes == DialogNodeTypes.Start)
            {
                state = true;
                break;
            }
        }

        if (!state)
        {
            LogPanel.Instance.AddError("Missing 'Start Conversation' node");
            Error.Instance.ShowError("Missing 'Start Conversation' node");
        }

        return state;
    }
}
