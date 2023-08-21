using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Gadget
    {
        private List<Item?> GadgetList = Enumerable.Repeat<Item?>(null , (int)PartName.End).ToList();
        
        public enum PartName
        {
            Head, Body, Arm, Leg, Boots,
            L_Hand, R_Hand,
            Consumable_1, Consumable_2, Consumable_3, End
        }

        void ShowGadget()
        {
            for (int i = 0 ; i < GadgetList.Count ; i++)
            {
                string? name = Enum.GetName<PartName>((PartName)i);
                name = name.PadRight(15);
                Console.Write($"{name}: ");
                Console.WriteLine(GadgetList[i] != null ? GadgetList[i].Name : "미장착");
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
        void UnEquip(PartName name)
        {
            GadgetList[(int)name] = null;
        }
        void Equip(PartName name , Item item)
        {
            GadgetList[(int)name] = item;
        }
        void EquipConsumable(Item item, int partNumber)
        {
            if(partNumber < 3)
            if (!TypeCheck(item , typeof(Consumable)))
                return;
            
            UnEquip(partNumber - 1 + PartName.Consumable_1);
            Equip(partNumber - 1 + PartName.Consumable_1 , item);
        }
        void EquipWear(Item item)
        {
            if (!TypeCheck(item , typeof(Wear)))
                return;

            //Enum을 맞춰줘서 이렇게해도 되지않을까?싶은마움
            UnEquip((PartName)( (Wear)item ).WType);
            Equip((PartName)( (Wear)item ).WType , item);

        }
        void EquipWeapon(Item item , PartName name = PartName.L_Hand)
        {
            if (!TypeCheck(item , typeof(Weapon)))
                return;

            switch (( (Weapon)item ).WType)
            {
                case WeaponTypes.Bow: // Bow는 무조건 왼손에 장착
                    UnEquip(PartName.R_Hand);
                    UnEquip(PartName.L_Hand);
                    Equip(PartName.L_Hand , item);
                    break;
                case WeaponTypes.OneHandSword:
                    if(name == PartName.L_Hand)
                    {
                        UnEquip(PartName.L_Hand);
                        Equip(PartName.L_Hand , item);
                    }
                    else if(name == PartName.R_Hand)
                    {
                        UnEquip(PartName.R_Hand);
                        Equip(PartName.R_Hand , item);
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
                    if (GadgetList[(int)PartName.L_Hand] == null
                         || ( (Weapon)GadgetList[(int)PartName.L_Hand] ).WType ==  WeaponTypes.OneHandSword)
                    {
                        UnEquip(PartName.R_Hand);
                        Equip(PartName.R_Hand , item);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
