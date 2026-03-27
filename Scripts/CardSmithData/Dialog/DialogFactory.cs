using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public class DialogFactory
    {
        public static IDialog BuildDialog(DialogNodeTypes type)
        {
            return type switch
            {
                DialogNodeTypes.Start => new StartDialog(),
                DialogNodeTypes.Say => new SayDialog(),
                DialogNodeTypes.Load => new LoadDialog(),
                DialogNodeTypes.Save => new SaveDialog(),
                DialogNodeTypes.End => new EndDialog(),
                DialogNodeTypes.ShowResponse => new ShowResponsesDialog(),
                DialogNodeTypes.Response => new ResponseDialog(),
                DialogNodeTypes.ResponseCondition => new ResponseConditionDialog(),
                DialogNodeTypes.TriggerAction => new TriggerActionDialog(),
                _ => null,
            };
        }
    }
}
