using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameObject
    {
        public void Init()
        {
            _gadget = new Gadget();
            _inventory = new Inventory();
        }




        private Gadget _gadget;
        private Inventory _inventory;
    }
}
