using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Item : GameObject
    {
        public Item(string name , ItemKinds itemKind)
        {
            Name = name;
            ItemKind = itemKind;
        }

        public string Name { get; private set; }
        public ItemKinds ItemKind { get; private set; }
    }
}
