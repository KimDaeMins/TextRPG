using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Consumable : Item
    {
        public Consumable(string name) : base(name, ItemKinds.Consumables)
        {
        }
    }
}
