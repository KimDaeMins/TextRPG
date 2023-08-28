using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    internal class Item : GameObject
    {
        public Item()
        {

        }
        public Item(string name , ItemKinds itemKind, int gold, bool buyAble)
        {
            Name = name;
            ItemKind = itemKind;
            Gold = gold;
            BuyAble = buyAble;
        }

        public virtual void ShowDetail()
        {
            Console.WriteLine( $"이름 : {Name}");
        }
        public virtual void ShowDetailInInven(int num)
        {
        }
        public virtual void ShowDetailBuy(int num)
        {

        }
        public virtual void ShowDetailSell(int num)
        {

        }
        public virtual void ShowDetailSell()
        {

        }
        public virtual int Sell()
        {
            return Gold * 85 / 100;
        }
        public void Equip(PartName partname)
        {
            EquipPart = partname;
        }
        public void UnEquip()
        {
            EquipPart = PartName.End;
        }
        public string Name { get; private set; }
        public ItemKinds ItemKind { get; private set; }
        public PartName EquipPart { get; private set; } = PartName.End;
        public int Gold { get; private set; }
        public bool BuyAble { get; private set; }
    }

    internal class Consumable : Item
    {
        public Consumable(string name, int count, int gold, bool buyAble = true) : base(name, ItemKinds.Consumables, gold, buyAble)
        {
            Count = count;
        }
        public int Count { get; private set; }
        public void Use()
        {
            Count--;
        }
        public void Drop(int count)
        {
            Count = Math.Max(Count - count, 0);
        }
        public override void ShowDetail()
        {
            Console.WriteLine($"{Name} 갯수 : {Count}");
        }
        public override void ShowDetailInInven(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;

            switch (EquipPart)
            {
                case PartName.Consumable_1:
                    countOfKorean += 2;
                    name += "[1번칸]";
                    break;
                case PartName.Consumable_2:
                    countOfKorean += 2;
                    name += "[2번칸]";
                    break;
                case PartName.Consumable_3:
                    countOfKorean += 2;
                    name += "[3번칸]";
                    break;
            }
            name += Name;
            if (!BuyAble)
                name += "[귀속]";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(26 - countOfKorean);

            Console.WriteLine($"{name}> 갯수 : {Count}");
        }
        public override void ShowDetailBuy(int num)
        {
            int countOfKorean = 0;
            string name = num.ToString() + ".";
            name += Name;

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            if (!BuyAble)
                name += "[귀속]";
            name = name.PadRight(22 - countOfKorean);
            Console.WriteLine($"{name}> | 가격 : {Gold} G");
        }
        public override void ShowDetailSell(int num)
        {
            string name = num.ToString() + ".";
            int countOfKorean = 0;
            switch (EquipPart)
            {
                case PartName.Consumable_1:
                    countOfKorean += 2;
                    name += "[1번칸]";
                    break;
                case PartName.Consumable_2:
                    countOfKorean += 2;
                    name += "[2번칸]";
                    break;
                case PartName.Consumable_3:
                    countOfKorean += 2;
                    name += "[3번칸]";
                    break;
            }
            name += Name;

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(22 - countOfKorean);

            if (BuyAble)
                Console.WriteLine($"{name}> 개수 : {Count}    | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 개수 : {Count}    | 귀속 아이템");
        }
        public override void ShowDetailSell()
        {
            string name = "";
            int countOfKorean = 0;
            if (EquipPart != PartName.End)
            {
                name += "[장착중]";
            }
            name += Name;

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(22 - countOfKorean);

            if (BuyAble)
                Console.WriteLine($"{name}> 개수 : {Count}    | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 개수 : {Count}    | 귀속 아이템");
        }
        public override int Sell()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("몇개 파시겠습니까? (0. 판매취소)");
            Console.Write(">> ");
            int count = 0;
            int.TryParse(Console.ReadLine(), out count);

            if (count <= 0)
            {
                return -1;
            }
            if (count > Count)
            {
                count = Count;
            }
            Count -= count;
            Console.WriteLine($"{count}개 판매 완료!");
            return count * (Gold * 85 / 100);
        }
    }
    internal class Other : Item
    {
        public Other(string name, int count, int gold, bool buyAble = true) : base(name, ItemKinds.Others, gold, buyAble)
        {
            Count = count;
        }
        public int Count { get; private set; }

        public void Drop(int count)
        {
            Count = Math.Max(Count - count, 0);
        }
        public override void ShowDetail()
        {
            Console.WriteLine($"이름 : {Name} 갯수 : {Count}");
        }

        public override void ShowDetailInInven(int num)
        {
            string name = num.ToString() + "." + Name;
            int countOfKorean = 0;
            if (!BuyAble)
                name += "[귀속]";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            name = name.PadRight(20 - countOfKorean);

            Console.WriteLine($"{name}> 갯수 : {Count}");
        }

        public override void ShowDetailBuy(int num)
        {
            int countOfKorean = 0;
            string name = num.ToString() + "." + Name;

            if (!BuyAble)
                name += "[귀속]";
            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
       
            name = name.PadRight(20 - countOfKorean);
            Console.WriteLine($"{name}> | 가격 : {Gold} G");
        }
        public override void ShowDetailSell(int num)
        {
            string name = num.ToString() + "." + Name;
            int countOfKorean = 0;

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(15 - countOfKorean);
            if(BuyAble)
                Console.WriteLine($"{name}> 개수 : {Count}    | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 개수 : {Count}    | 귀속 아이템");
        }
        public override void ShowDetailSell()
        {
            string name = "";
            int countOfKorean = 0;
            name += Name;

            for (int i = 0; i < Name.Length; i++)
            {
                byte oF = (byte)((Name[i] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }

            name = name.PadRight(15 - countOfKorean);

            if (BuyAble)
                Console.WriteLine($"{name}> 개수 : {Count}    | 판매가 : {Gold * 85 / 100} G");
            else
                Console.WriteLine($"{name}> 개수 : {Count}    | 귀속 아이템");
        }
        public override int Sell()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("몇개 파시겠습니까? (0. 판매취소)");
            Console.Write(">> ");
            int count = 0;
            int.TryParse(Console.ReadLine(), out count);

            if (count <= 0)
            {
                return -1;
            }
            if (count > Count)
            {
                count = Count;
            }
            Count -= count;
            Console.WriteLine($"{count}개 판매 완료!");
            return count * (Gold * 85 / 100);
        }
    }
}
