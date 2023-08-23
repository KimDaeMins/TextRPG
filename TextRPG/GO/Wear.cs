using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    internal class Wear : Item
    {
        public Wear(WearTypes type, string name , int level , int hp , int mp , int def, int gold, bool buyAble = true) : base(name, (ItemKinds)type, gold, buyAble)
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

        public override void ShowDetail()
        {
            Console.WriteLine($"{Name}({WType.ToString()}) 요구레벨 : {Level} 체력 : {Hp} 마나 : {Mp} 방어력 : {Def}");
        }

        public override void ShowDetailInInven(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            if (EquipPart != PartName.End)
            {
                countOfKorean += 3;
                name += "[장착중]";
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
            name = name.PadRight(30 - countOfKorean);

            string hp = Hp.ToString();
            hp = hp.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            string mp = Mp.ToString();
            mp = mp.PadRight(5);
            Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def}");
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
            name = name.PadRight(30 - countOfKorean);

            string hp = Hp.ToString();
            hp = hp.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            string mp = Mp.ToString();
            mp = mp.PadRight(5);
            Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def} | 가격 : {Gold} G");

        }
        public override void ShowDetailSell(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            if (EquipPart != PartName.End)
            {
                countOfKorean += 3;
                name += "[장착중]";
            }
            name += Name + "(" + WType.ToString() + ")";

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            name = name.PadRight(26 - countOfKorean);

            string hp = Hp.ToString();
            hp = hp.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            string mp = Mp.ToString();
            mp = mp.PadRight(5);
            if (BuyAble)
                Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def} | 판매가 {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def} | 귀속 아이템");
        }
        public override void ShowDetailSell()
        {
            string name = "";
            int countOfKorean = 0;
            if (EquipPart != PartName.End)
            {
                countOfKorean += 3;
                name += "[장착중]";
            }
            name += Name + "(" + WType.ToString() + ")";

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            name = name.PadRight(26 - countOfKorean);

            string hp = Hp.ToString();
            hp = hp.PadRight(5);
            string level = Level.ToString();
            level = level.PadRight(3);
            string def = Def.ToString();
            def = def.PadRight(5);
            string mp = Mp.ToString();
            mp = mp.PadRight(5);
            if (BuyAble)
                Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def} | 판매가 {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 요구레벨 : {level} 체력 : {hp} 마나 : {mp} 방어력 : {def} | 귀속 아이템");
        }
    }
    //신발이 신발으로써 특수한 로직이 있다면 분기할게
}
