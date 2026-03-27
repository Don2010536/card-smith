using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public interface ITriggerAction : IDialog
    {
        int ActionID { get; set; }
    }
}
