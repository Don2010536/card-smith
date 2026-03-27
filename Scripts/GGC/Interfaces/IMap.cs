using System.Collections.Generic;

namespace GGC.Interfaces
{
    public interface IMap<T0, T1>
    {
        public Dictionary<T0, T1> Map { get; set; }
    }
}
