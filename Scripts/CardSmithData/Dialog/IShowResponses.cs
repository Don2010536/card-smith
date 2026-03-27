using CardSmithData.Dialog.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog
{
    public interface IShowResponses : IDialog
    {
        List<IResponse> Responses { get; set; }
    }
}
