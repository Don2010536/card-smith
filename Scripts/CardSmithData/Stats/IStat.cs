using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IStat<T> : ISavable, ILoadable
{
    string Name { get; set; }
    string DataType { get; set; }

    T Value { get; set; }
}