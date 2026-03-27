using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSmith.Data
{
    public class Bucket<Collection, DataType>(string name, Collection collection) where Collection : ICollection<DataType>
    {
        public string Name { get; set; } = name;
        public Collection Container { get; set; } = collection;
    }
}