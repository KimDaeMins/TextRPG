using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextRPG
{
    internal class InventoryScene
    {

        public void ShowInven(Inventory inven, Gadget gadget)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1.방어구 2.무기 3.소모품 4.기타아이템 0.나가기");
                int select;
                int.TryParse(Console.ReadLine(), out select);
                if (select == 0)
                    return;
                switch (select)
                {
                    case 1:
                        ShowWear(inven, gadget);
                        break;
                    case 2:
                        ShowWeapon(inven, gadget);
                        break;
                    case 3:
                        ShowConsumable(inven, gadget);
                        break;
                    case 4:
                        ShowOther(inven);
                        break;
                }
            }
        }
        private int Choice(int count)
        {
            while (true)
            {
                int select;
                Console.WriteLine("---------------------------");
                Console.Write("선택 : ");
                int.TryParse(Console.ReadLine(), out select);
                if (select == 0)
                    return 0;
                if (select > count)
                {
                    Console.WriteLine("입력오류");
                    continue;
                }
                return select;
            }
        }
        public void ShowWear(Inventory inven, Gadget gadget)
        {
            while (true)
            {
                Console.Clear();
                int count = inven.ShowItems(ItemKinds.Head, ItemKinds.Boots);

                Console.WriteLine("0.뒤로가기");
                int select = Choice(count);
                if (select == 0)
                {
                    return;
                }

                int j = 0;
                while (select - inven.GetItemCount(j) > 0)
                {
                    select -= inven.GetItemCount(j);
                    j++;
                }
                select -= 1; //얘가 인덱스가되고
                             //j 가 리스트내부인덱스

                Console.WriteLine("---------------------------");
                Console.WriteLine("1.장착하기 2.해제하기 3.버리기 0.뒤로가기");
                Console.WriteLine("---------------------------");
                Console.Write("입력 : ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);

                inven.EquipOrDropWear(gadget, j, select, choice);
            }
        }
        public void ShowWeapon(Inventory inven, Gadget gadget)
        {
            while(true)
            {
                Console.Clear();
                int count = inven.ShowItems(ItemKinds.Weapon, ItemKinds.Weapon);
                Console.WriteLine("0.뒤로가기");

                int select = Choice(count);
                if (select == 0)
                    return;
                select -= 1;
                Console.WriteLine("---------------------------");
                Console.WriteLine("1.왼손장착 2.오른손장착 3.장착해제 4.버리기 0.뒤로가기");
                Console.WriteLine("---------------------------");
                Console.Write("입력 : ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);

                inven.EquipOrDropWeapon(gadget, select, choice);
            }
        }
        public void ShowConsumable(Inventory inven, Gadget gadget)
        {
            while (true)
            {
                Console.Clear();
                int count = inven.ShowItems(ItemKinds.Consumables, ItemKinds.Consumables);
                Console.WriteLine("0.뒤로가기");

                int select = Choice(count);
                if (select == 0)
                    return;
                select -= 1;

                Console.WriteLine("---------------------------");
                Console.WriteLine("1.1번칸장착 2.2번칸장착 3.3번칸장착 4.장착해제 5.버리기 0.뒤로가기");
                Console.WriteLine("---------------------------");
                Console.Write("입력 : ");

                int choice;
                int.TryParse(Console.ReadLine(), out choice);

                inven.EquipOrDropConsumable(gadget, select, choice);
            }
        }
        public void ShowOther(Inventory inven)
        {
            while (true)
            {
                Console.Clear();
                int count = inven.ShowItems(ItemKinds.Others, ItemKinds.Others);
                Console.WriteLine("0.뒤로가기");

                int select = Choice(count);
                if (select == 0)
                    return;
                select -= 1;

                Console.WriteLine("---------------------------");
                Console.WriteLine("1.버리기 0.뒤로가기");
                Console.WriteLine("---------------------------");
                Console.Write("입력 : ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);

                inven.DropOther(select, choice);
            }
        }
    }
}
