using GGC.Interfaces;
using System.Collections.Generic;

namespace CardSmithData.Dialog
{
    public interface IDialog : ISavable, GGC.Interfaces.ILoadable
    {
        public DialogNodeTypes DialogNodeType { get; set; }
        public List<IDialog> RightConnections { get; set; }
    }
}
