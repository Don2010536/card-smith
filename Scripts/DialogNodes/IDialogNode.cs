using CardSmithData.Dialog;
using System.IO;

public interface IDialogNode
{
    public DialogNodeTypes DialogNodeTypes { get; set; }
    public IDialog DialogData { get; set; }

    public void BuildDialog();
    public void FillConnections();

    public void SaveNode(ref BinaryWriter writer);

    public void LoadNoad(ref BinaryReader reader);
}
