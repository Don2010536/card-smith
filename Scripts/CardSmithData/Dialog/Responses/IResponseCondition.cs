using GGC.Interfaces;
using System;

namespace CardSmithData.Dialog.Responses
{
    public interface IResponseCondition : IDialog
    {
        public bool CustomStatUsed { get; set; }

        public StatTypes StatType { get; set; }

        public string StatKey { get; set; }

        public ComparissonTypes ComparissonType { get; set; }

        public string ComparrisonValue { get; set; }
    }
}