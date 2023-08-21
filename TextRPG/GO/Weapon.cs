using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Weapon : Item
    {
       public Weapon(string name, int att, int def, int level, WeaponTypes type) : base(name, ItemKinds.Equips)
        {
            Att = att;
            Def = def;
            Level = level;
            WType = type;
        }
        public int Att { get; protected set; }
        public int Def { get; protected set; }
        public int Level { get; protected set; }
        public WeaponTypes WType { get; protected set; }
        
    }

    internal class Bow : Weapon
    {
        public Bow(string name, int level, int att, int def = 0) : base(name, att ,def, level, WeaponTypes.Bow)
        {
        }

    }
    internal class OneHandSword : Weapon
    {
        public OneHandSword(string name , int level , int att , int def = 0) : base(name , att , def , level , WeaponTypes.OneHandSword)
        {
        }
    }
    internal class TwoHandSword : Weapon
    {
        public TwoHandSword(string name , int level , int att , int def = 0) : base(name , att , def , level , WeaponTypes.TwoHandSword)
        {
        }
    }
    internal class Staff : Weapon
    {
        public Staff(string name , int level , int att , int def = 0) : base(name , att , def , level , WeaponTypes.Staff)
        {
        }
    }
    internal class Shield : Weapon
    {
        public Shield(string name , int level , int att , int def ) : base(name , att , def , level , WeaponTypes.Shield)
        {
        }
    }


}