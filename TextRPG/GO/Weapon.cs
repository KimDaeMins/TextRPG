using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Weapon : Item
    {
       public Weapon(string name, int att, int def, int level, WeaponTypes type, int gold, bool buyAble) : base(name, ItemKinds.Weapon, gold, buyAble)
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
        public bool IsOneHand { get; protected set; }
        public override void ShowDetail()
        {
            Console.WriteLine($"{Name}({WType.ToString()}) 요구레벨 : {Level} 공격력 : {Att} 방어력 : {Def}");
        }
        public override void ShowDetailInInven(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            switch (EquipPart)
            {
                case PartName.L_Hand:
                    countOfKorean += 2;
                    if (IsOneHand)
                        name += "[왼손]";
                    else
                        name += "[양손]";
                    break;
                case PartName.R_Hand:
                    countOfKorean += 3;
                    name += "[오른손]";
                    break;
            }
            name += Name + "(" + WType.ToString() + ")";
            if (!BuyAble)
                name += "[귀속]";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            name = name.PadRight(36 - countOfKorean);

            string att = Att.ToString();
            att = att.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def}");
        }
        public override void ShowDetailBuy(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            name += Name + "(" + WType.ToString() + ")";

            if (!BuyAble)
                name += "[귀속]";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            name = name.PadRight(36 - countOfKorean);

            string att = Att.ToString();
            att = att.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
                Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def} | 가격 : {Gold} G");
        }
        public override void ShowDetailSell(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            switch (EquipPart)
            {
                case PartName.L_Hand:
                    countOfKorean += 2;
                    if (IsOneHand)
                        name += "[왼손]";
                    else
                        name += "[양손]";
                    break;
                case PartName.R_Hand:
                    countOfKorean += 3;
                    name += "[오른손]";
                    break;
            }
            name += Name + "(" + WType.ToString() + ")";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(32 - countOfKorean);

            string att = Att.ToString();
            att = att.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            if(BuyAble)
                Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def} | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def} | 귀속 아이템");
        }
        public override void ShowDetailSell()
        {
            string name = "";
            int countOfKorean = 0;
            switch (EquipPart)
            {
                case PartName.L_Hand:
                    countOfKorean += 2;
                    if (IsOneHand)
                        name += "[왼손]";
                    else
                        name += "[양손]";
                    break;
                case PartName.R_Hand:
                    countOfKorean += 3;
                    name += "[오른손]";
                    break;
            }
            name += Name + "(" + WType.ToString() + ")";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(32 - countOfKorean);

            string att = Att.ToString();
            att = att.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);

            if (BuyAble)
                Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def} | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 요구레벨 : {level} 공격력 : {att} 방어력 : {def} | 귀속 아이템");
        }

    }

    internal class Bow : Weapon
    {
        public Bow(string name, int level, int att, int def = 0,int gold = 0, bool buyAble = true) : base(name, att, def, level, WeaponTypes.OneHandSword, gold, buyAble)
        {
            IsOneHand = false;
        }

    }
    internal class OneHandSword : Weapon
    {
        public OneHandSword(string name, int level, int att, int def = 0, int gold = 0, bool buyAble = true) : base(name, att, def, level, WeaponTypes.Bow, gold, buyAble)
        {
            IsOneHand = true;
        }
    }
    internal class TwoHandSword : Weapon
    {
        public TwoHandSword(string name, int level, int att, int def = 0, int gold = 0, bool buyAble = true) : base(name, att, def, level, WeaponTypes.TwoHandSword, gold, buyAble)
        {
            IsOneHand = false;
        }
    }
    internal class Staff : Weapon
    {
        public Staff(string name, int level, int att, int def = 0, int gold = 0, bool buyAble = true) : base(name, att, def, level, WeaponTypes.Staff, gold, buyAble)
        {
            IsOneHand = false;
        }
    }
    internal class Shield : Weapon
    {
        public Shield(string name, int level, int att, int def, int gold = 0, bool buyAble = true) : base(name, att, def, level, WeaponTypes.Shield, gold, buyAble)
        {
            IsOneHand = true;
        }
    }


}