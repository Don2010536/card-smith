using Godot;
using System.Collections.Generic;

namespace GGC.Interfaces
{
    public interface IStore<T0, T1>
    {
        public T0 CurrentSelection { get; set; }
        public PackedScene PurchaseNode { get; set; }

        public void GenerateItemsFromEnumerable(IEnumerable<T1> purchasable);
        public void Purchase_OnPressed(object sender, System.EventArgs e);
    }
}
