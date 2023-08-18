using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    internal class Wear : Item
    {
        public Wear(WearTypes type, string name , int level , int hp , int mp , int def) : base(name, ItemKinds.Equips)
        {
            WType = type;
            Level = level;
            Hp = hp;
            Mp = mp;
            Def = def;
        }
        public int Hp { get; private set; }
        public int Mp { get; private set; }
        public int Def { get; private set; }
        public int Level { get; private set; }
        public WearTypes WType { get; private set; }
    }
    //신발이 신발으로써 특수한 로직이 있다면 분기할게
}
