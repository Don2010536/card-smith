using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmithData.Dialog.Responses
{
    public interface IResponse : IDialog
    {
        public string RespnseText { get; set; }

        public LogicalOperators LogicalOperator { get; set; }
        public List<IResponseCondition> Conditions { get; set; }
    }
}