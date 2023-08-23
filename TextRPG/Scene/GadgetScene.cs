using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GadgetScene
    {

        public void ShowGadget(Inventory inven, Gadget gadget)
        {
            while (true)
            {
                gadget.ShowGadget();
                Console.WriteLine("0. 나가기");
                Console.WriteLine("------------------------");
                Console.Write("입력 : ");
                int select;
                int.TryParse(Console.ReadLine(), out select);
                if (select == 0)
                    return;

                int index = select - 1;
                Console.WriteLine("------------------------");
                if (gadget.ShowDetail(index))
                {
                    Console.WriteLine("1.장비 교체 2.장착 해제 0.뒤로가기");
                }
                else
                {
                    Console.WriteLine("1.장착 하기 0.뒤로 가기");
                }
                int.TryParse(Console.ReadLine(), out select);


                if (select == 2)
                {
                    gadget.UnEquip(index);
                }
                else if (select == 1)
                {
                    gadget.EquipforInven((PartName)index, inven.EquipSwap(index));
                }

                Console.Clear();
                continue;
            }
        }
    }
}
