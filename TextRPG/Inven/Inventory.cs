using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Inventory
    {
       
        Dictionary<ItemKinds , List<Item>> Inven = new Dictionary<ItemKinds , List<Item>>();
        public void Init()
        {
            foreach(ItemKinds kind in Enum.GetValues(typeof(ItemKinds)))
            {
                Inven.Add(kind, new List<Item>());
            }
        }
        private bool TypeCheck(Item item, Type compareType)
        {
            Type? type = item.GetType();
            while (true)
            {
                if (type == compareType)
                    return true;

                type = type.BaseType;

                if (type == null)
                {
                    //의상클래스를 찾지못했다.
                    return false; ;
                }
            }
        }
        public bool Rooting(Item item)
        {
            Inven[item.ItemKind].Add(item);
            return true;
        }
        public Item GetItem(ItemKinds kind, int index)
        {
            return Inven[kind][index];
        }
        public Item GetItem(int kind, int index)
        {
            return Inven[(ItemKinds)kind][index];
        }
        public Item? EquipSwap(int index)
        {
            if(index < (int)PartName.L_Hand)
            {
                return ShowInvenPart((ItemKinds)index);
            }
            else if(index < (int)PartName.Consumable_1) 
            {
                return ShowInvenPart(ItemKinds.Weapon);
            }
            else
            {
                return ShowInvenPart(ItemKinds.Consumables);
            }
            
        }
        public int Sell(Gadget gadget, int kind, int index)
        {
            Item item = Inven[(ItemKinds)kind][index];
            if(!item.BuyAble)
            {
                Console.WriteLine("귀속 아이템입니다");
                Console.ReadLine();
                return -1;
            }
            if (kind == (int)ItemKinds.Consumables)
            {
                int gold = item.Sell();
                if(((Consumable)item).Count == 0)
                {
                    gadget.UnEquip(item);
                    Inven[(ItemKinds)kind].RemoveAt(index);
                }
                return gold;
            }
            if (kind == (int)ItemKinds.Others)
            {
                int gold = item.Sell();
                if (((Other)item).Count == 0)
                {
                    Inven[(ItemKinds)kind].RemoveAt(index);
                }
                return gold;
            }
            else
            {
                int gold = item.Sell();
                gadget.UnEquip(item);
                Inven[(ItemKinds)kind].RemoveAt(index);
                return gold;
            }
        }

        public Item? ShowInvenPart(ItemKinds index)
        {
            int count = 1;
            foreach(Item item in Inven[index])
            {
                item.ShowDetailInInven(count++);
            }
            Console.Write("입력 : ");
            int.TryParse(Console.ReadLine(), out count);
            if(count > 0 || count <= Inven[index].Count)
            {
                return Inven[index][count - 1];
            }
            return null;
        }

        public int ShowItems(ItemKinds startItemKind, ItemKinds endItemKind)
        {
            int count = 1;
            for (int i = (int)startItemKind; i <= (int)endItemKind ; ++i)
            {
                foreach (Item item in Inven[(ItemKinds)i])
                {
                    item.ShowDetailInInven(count++);
                }
            }
            return count - 1;
        }
        public int ShowSell(ItemKinds startItemKind, ItemKinds endItemKind)
        {
            int count = 1;
            for (int i = (int)startItemKind; i <= (int)endItemKind; ++i)
            {
                foreach (Item item in Inven[(ItemKinds)i])
                {
                    item.ShowDetailSell(count++);
                }
            }
            return count - 1;
        }
        public int GetItemCount(ItemKinds itemKind)
        {
            return Inven[itemKind].Count;
        }
        public int GetItemCount(int index)
        {
            return Inven[(ItemKinds)index].Count;
        }
        public void EquipOrDropWear(Gadget gadget, int dicIndex, int listIndex, int choice)
        {
            if (choice == 1)
            {
                gadget.EquipWear(Inven[(ItemKinds)dicIndex][listIndex]);
            }
            else if (choice == 2)
            {
                gadget.UnEquip(Inven[(ItemKinds)dicIndex][listIndex]);
            }
            else if (choice == 3)
            {
                if (Inven[(ItemKinds)dicIndex][listIndex].EquipPart == PartName.End)
                {
                    Inven[(ItemKinds)dicIndex].RemoveAt(listIndex);
                }
            }
        }
        public void EquipOrDropWeapon(Gadget gadget, int listIndex, int choice)
        {
            if (choice == 1)
            {
                gadget.EquipWeapon(Inven[ItemKinds.Weapon][listIndex]);
            }
            else if (choice == 2)
            {
                gadget.EquipWeapon(Inven[ItemKinds.Weapon][listIndex], PartName.R_Hand);
            }
            else if (choice == 3)
            {
                gadget.UnEquip(Inven[ItemKinds.Weapon][listIndex]);
            }
            else if (choice == 4)
            {
                if (Inven[ItemKinds.Weapon][listIndex].EquipPart == PartName.End)
                    Inven[ItemKinds.Weapon].RemoveAt(listIndex);
            }
        }

        public void EquipOrDropConsumable(Gadget gadget, int listIndex, int choice)
        {
            if (choice > 0 && choice < 4)
            {
                gadget.EquipConsumable(Inven[ItemKinds.Consumables][listIndex], choice);
            }
            else if(choice == 4)
            {
                gadget.UnEquip(Inven[ItemKinds.Consumables][listIndex]);
            }
            else if (choice == 5)
            {
                if (Inven[ItemKinds.Consumables][listIndex].EquipPart == PartName.End)
                {
                    Console.WriteLine($"현재갯수{((Consumable)Inven[ItemKinds.Consumables][listIndex]).Count}");
                    Console.Write("몇개? : ");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice >= 0 && choice <= ((Consumable)Inven[ItemKinds.Consumables][listIndex]).Count)
                    {
                        ((Consumable)Inven[ItemKinds.Consumables][listIndex]).Drop(choice);
                        if (((Consumable)Inven[ItemKinds.Consumables][listIndex]).Count == 0)
                            Inven[ItemKinds.Consumables].RemoveAt(listIndex);
                    }
                }
            }
        }
        public void DropOther(int listIndex, int choice)
        {
            if (choice == 1)
            {
                Console.WriteLine($"현재갯수{((Other)Inven[ItemKinds.Others][listIndex]).Count}");
                Console.Write("몇개? : ");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice >= 0 && choice <= ((Other)Inven[ItemKinds.Others][listIndex]).Count)
                {
                    ((Other)Inven[ItemKinds.Others][listIndex]).Drop(choice);
                    if (((Other)Inven[ItemKinds.Others][listIndex]).Count == 0)
                        Inven[ItemKinds.Others].RemoveAt(listIndex);
                }
            }
        }

    }
}
