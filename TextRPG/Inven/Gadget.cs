using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    internal class Gadget
    {

        public Action<int, int, int, int> UnEquipAction;
        public Action<int, int, int, int> EquipAction;
        public delegate int Del();
        public Del GetLevel;
        private List<Item?> GadgetList = Enumerable.Repeat<Item?>(null , (int)PartName.End).ToList();
        
        public Item? GetGadget(int index)
        {
            return GadgetList[index];
        }
        public bool ShowDetail(int a)
        {
            if(a >= (int)PartName.End || a < 0)
            {
                Console.WriteLine("정확한 숫자를 입력해주세요");
                return false;
            }

            if (GadgetList[a] is null)
            {   
                Console.WriteLine("미장착상태");
                return false;
            }
            GadgetList[a]!.ShowDetail();

            return true;
        }
        public void ShowGadget()
        {
            Console.Clear();
            for (int i = 0 ; i < GadgetList.Count ; i++)
            {
                string name = (i + 1).ToString() + ". ";
                name += Enum.GetName<PartName>((PartName)i);
                name = name!.PadRight(18);
                Console.Write($"{name}: ");
                if(i == (int)PartName.R_Hand && GadgetList[i - 1] is not null && ((Weapon)GadgetList[i - 1]!).IsOneHand == false)
                    Console.WriteLine($"{GadgetList[i - 1]!.Name}(양손무기)");
                else
                    Console.WriteLine(GadgetList[i] is not null ? GadgetList[i]!.Name : "미장착");
            }
        }
        bool TypeCheck(Item item, Type compareType)
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
        public void UnEquip(PartName name)
        {
            if (GadgetList[(int)name] is not null)
            {
                if (name < PartName.L_Hand)
                {
                    UnEquipAction.Invoke(0, ((Wear)GadgetList[(int)name]!).Def, ((Wear)GadgetList[(int)name]!).Hp, ((Wear)GadgetList[(int)name]!).Mp);
                }
                else if (name < PartName.Consumable_1)
                {
                    UnEquipAction.Invoke(((Weapon)GadgetList[(int)name]!).Att, ((Weapon)GadgetList[(int)name]!).Def
                        , 0, 0);
                }
                GadgetList[(int)name]!.UnEquip();
            }
            GadgetList[(int)name] = null;
        }
        public void UnEquip(Item item)
        {
            if (item.EquipPart == PartName.End)
                return;

            if (TypeCheck(item, typeof(Weapon)))
            {
                UnEquipAction.Invoke(((Weapon)item).Att, ((Weapon)item).Def, 0, 0);
                if (GadgetList[(int)PartName.L_Hand] == item)
                {
                    GadgetList[(int)PartName.L_Hand]!.UnEquip();
                    GadgetList[(int)PartName.L_Hand] = null;
                }
                else if (GadgetList[(int)PartName.R_Hand] == item)
                {
                    GadgetList[(int)PartName.R_Hand]!.UnEquip();
                    GadgetList[(int)PartName.R_Hand] = null;
                }
            }
            if (TypeCheck(item, typeof(Wear)))
            {
                UnEquipAction.Invoke(0, ((Wear)item).Def, ((Wear)item).Hp, ((Wear)item).Mp);
                GadgetList[(int)item.ItemKind]!.UnEquip();
                GadgetList[(int)item.ItemKind] = null;
            }
            if (TypeCheck(item, typeof(Consumable)))
            {
                if (GadgetList[(int)PartName.Consumable_1] == item)
                {
                    GadgetList[(int)PartName.Consumable_1]!.UnEquip();
                    GadgetList[(int)PartName.Consumable_1] = null;
                }
                else if (GadgetList[(int)PartName.Consumable_2] == item)
                {
                    GadgetList[(int)PartName.Consumable_2]!.UnEquip();
                    GadgetList[(int)PartName.Consumable_2] = null;
                }
                else
                {
                    GadgetList[(int)PartName.Consumable_3]!.UnEquip();
                    GadgetList[(int)PartName.Consumable_3] = null;
                }
            }
        }
        public void UnEquip(int index)
        {

            if (GadgetList[index] is not null)
            {
                if(index < (int)PartName.L_Hand)
                {
                    UnEquipAction.Invoke(0, ((Wear)GadgetList[index]!).Def, ((Wear)GadgetList[index]!).Hp, ((Wear)GadgetList[index]!).Mp);
                }
                else if(index < (int)PartName.Consumable_1)
                {
                    UnEquipAction.Invoke(((Weapon)GadgetList[index]!).Att, ((Weapon)GadgetList[index]!).Def
                        , 0, 0);
                }
                GadgetList[index]!.UnEquip();
            }
            GadgetList[index] = null;
        }

        void Equip(PartName name , Item item)
        {
            GadgetList[(int)name] = item;

            if (name < PartName.L_Hand)
            {
                EquipAction.Invoke(0, ((Wear)item).Def, ((Wear)item).Hp, ((Wear)item).Mp);
            }
            else if (name < PartName.Consumable_1)
            {
                EquipAction.Invoke(((Weapon)item).Att, ((Weapon)item).Def, 0, 0);
            }

            item.Equip(name);
        }
        void Equip(int index, Item? item)
        {
            if (item is not null)
            {
                if (index < (int)PartName.L_Hand)
                {
                    EquipAction.Invoke(0, ((Wear)item).Def, ((Wear)item).Hp, ((Wear)item).Mp);
                }
                else if (index < (int)PartName.Consumable_1)
                {
                    EquipAction.Invoke(((Weapon)item).Att, ((Weapon)item).Def, 0, 0);
                }

                GadgetList[index] = item;
                item.Equip((PartName)index);
            }
        }
        public void EquipConsumable(Item item, int partNumber)
        {
            if (item.EquipPart != PartName.End)
                UnEquip(item);

            if(partNumber < 3)
            if (!TypeCheck(item , typeof(Consumable)))
                return;
            
            UnEquip(partNumber - 1 + PartName.Consumable_1);
            Equip(partNumber - 1 + PartName.Consumable_1 , item);
        }
        public void EquipConsumable(Item item, PartName part)
        {
            if (!TypeCheck(item, typeof(Consumable)))
                return;
            if (item.EquipPart != PartName.End)
                UnEquip(item);

            UnEquip(part);
            Equip(part, item);
        }
        public void EquipWear(Item item)
        {
            if (!TypeCheck(item , typeof(Wear)))
                return;
            if (((Wear)item).Level > GetLevel())
            {
                Console.WriteLine("레벨이 낮습니다");
                Console.ReadLine();
                return;
            }
            //Enum을 맞춰줘서 이렇게해도 되지않을까?싶은마움
            UnEquip((PartName)( (Wear)item ).WType);
            Equip((PartName)( (Wear)item ).WType , item);

        }
        public void EquipWeapon(Item item , PartName name = PartName.L_Hand)
        {
            if (!TypeCheck(item , typeof(Weapon)))
                return;
            if (((Weapon)item).Level > GetLevel())
            {
                Console.WriteLine("레벨이 낮습니다");
                Console.ReadLine();
                return;
            }

            switch (( (Weapon)item ).WType)
            {
                case WeaponTypes.Bow: // Bow는 무조건 왼손에 장착
                    UnEquip(PartName.R_Hand);
                    UnEquip(PartName.L_Hand);
                    Equip(PartName.L_Hand , item);
                    break;
                case WeaponTypes.OneHandSword:
                    if (name == PartName.L_Hand)
                    {
                        UnEquip(PartName.L_Hand);
                        Equip(PartName.L_Hand, item);
                    }
                    else if (name == PartName.R_Hand)
                    {
                        if (GadgetList[(int)PartName.L_Hand] is not null)
                        {
                            if (!((Weapon)GadgetList[(int)PartName.L_Hand]!).IsOneHand)
                            {
                                UnEquip(PartName.L_Hand);
                            }
                        }
                        UnEquip(PartName.R_Hand);
                        Equip(PartName.R_Hand, item);
                    }
                    break;
                case WeaponTypes.TwoHandSword:
                    UnEquip(PartName.R_Hand);
                    UnEquip(PartName.L_Hand);
                    Equip(PartName.L_Hand , item);
                    break;
                case WeaponTypes.Staff:
                    UnEquip(PartName.R_Hand);
                    UnEquip(PartName.L_Hand);
                    Equip(PartName.L_Hand , item);
                    break;
                case WeaponTypes.Shield:
                    if (GadgetList[(int)PartName.L_Hand] is null
                         || !( (Weapon)GadgetList[(int)PartName.L_Hand]! ).IsOneHand)
                    {
                        UnEquip(PartName.L_Hand);
                    }
                    UnEquip(PartName.R_Hand);
                    Equip(PartName.R_Hand, item);
                    break;
                default:
                    break;
            }
        }
        public void EquipforInven(PartName partIndex, Item? item)
        {
            if (item is null || item.EquipPart != PartName.End)
                return;
          
            if (partIndex < PartName.L_Hand)
                EquipWear(item);
            else if (partIndex < PartName.Consumable_1)
                EquipWeapon(item, partIndex);
            else
                EquipConsumable(item, partIndex);
        }
    }
}
