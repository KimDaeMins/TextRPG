using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum PartName
    {
        Head, Body, Arm, Leg, Boots,
        L_Hand, R_Hand,
        Consumable_1, Consumable_2, Consumable_3, End
    }

    public enum ItemKinds
    {
        Head, Body, Arm, Leg, Boots, Weapon, Consumables, Others , End
    }
    public enum WearTypes
    {
        Head, Body, Arm, Leg, Boots, End
    }
    public enum WeaponTypes
    {
        Bow, OneHandSword, TwoHandSword, Staff, Shield
    }
}
