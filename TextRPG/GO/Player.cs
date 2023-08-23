using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameObject
    {
        public void Init(String name)
        {
            Name = name;
            Level = 1;
            Att = 10;
            Def = 5;
            Hp = 100;
            Mp = 100;
            NHp = 100;
            NMp = 100;
            Gold = 1500;
            _gadget.UnEquipAction += UnEquip;
            _gadget.EquipAction += Equip;
            _gadget.GetLevel = GetLevel;
            //Item a = new Bow("기본 활", 1, 10);
            //_gadget.EquipWeapon(a);
            _inventory.Init();
            //_inventory.Rooting(new OneHandSword("한손검", 1, 10));
            _inventory.Rooting(new OneHandSword("낡은 한손검", 1, 2));
            //_inventory.Rooting(a);
            //_inventory.Rooting(new TwoHandSword("양손검", 1, 10));
            _inventory.Rooting(new Staff("스태프", 1, 10, 800));
            _inventory.Rooting(new Shield("방패", 1, 0, 10, 800));
            _inventory.Rooting(new Consumable("체력 물약", 10, 500));
            _inventory.Rooting(new Consumable("마나 물약", 10, 500));
            _inventory.Rooting(new Other("똥", 100, 100));
            _inventory.Rooting(new Other("특별한 똥", 100, 100, false));
            _inventory.Rooting(new Wear(WearTypes.Head, "투구", 1, 3, 0, 2, 500));
            _inventory.Rooting(new Wear(WearTypes.Arm, "장갑", 1, 0, 3, 2, 500));
            Item item = new Wear(WearTypes.Body, "무쇠 갑옷", 1, 0, 0, 5, 1000);
            _inventory.Rooting(item);
            _gadget.EquipWear(item);
            //_inventory.Rooting(new Wear(WearTypes.Leg, "바지", 1, 5, 5, 5));
            //_inventory.Rooting(new Wear(WearTypes.Boots, "신발", 1, 10, 0, 2));
 
        }
        public int GetLevel()
        {
            return Level;
        }
        public void UnEquip(Item item)
        {
            _gadget.UnEquip(item);
        }
        public int Sell(int kind , int index)
        {
           return _inventory.Sell(_gadget, kind, index);
        }
        public void UnEquip(int att, int def, int hp, int mp)
        {
            EHp -= hp;
            EAtt -= att;
            EMp -= mp;
            EDef -= def;

            //현재수치조정
            NHp -= hp;
            NMp -= mp;
        }
        public void Equip(int att, int def, int hp, int mp)
        {
            EHp += hp;
            EAtt += att;
            EMp += mp;
            EDef += def;

            //현재수치조정
            NHp += hp;
            NMp += mp;
        }
        public int HpDown(int hp)
        {
            NHp -= hp;
            if (NHp <= 0)
            {
                //죽었다!
                NHp = Hp + EHp;
                return 0;
            }
            return NHp;
        }
        public int HpUp(int hp)
        {
            int AddHp = hp;
            if(NHp + hp > EHp + Hp)
            {
                AddHp = EHp + Hp - NHp;
            }
            NHp += AddHp;
            return AddHp;
        }
        public void AddGold(int gold)
        {
            Console.WriteLine($"({Gold} G) -> ({Gold + gold} G)");
            Gold += gold;
        }
        public void SetGold(int gold)
        {
            Console.WriteLine($"({Gold} G) -> ({gold} G)");
            Gold = gold;
        }
        public void LevelUp()
        {
            ++Level;
            Att += 1;
            Def += 1;
        }
        public void ShowStatus()
        {
            Console.WriteLine($"Lv     : {Level}");
            Console.WriteLine($"Chad   : {Name}");
            Console.WriteLine($"공격력 : {Att + EAtt} (+{EAtt})");
            Console.WriteLine($"방어력 : {Def + EDef} (+{EDef})");
            Console.WriteLine($"체 력  : {NHp} / {Hp + EHp} (+{EHp})");
            Console.WriteLine($"마 나  : {NMp} / {Mp + EMp} (+{EMp})");
            Console.WriteLine($"Gold   : {Gold}");
        }
        public void ShowDungeonStatus()
        {
            //전투중엔 장비변경이 없으니 쇼스테이터스에서만 장비스텟세팅이들어감
            Console.WriteLine($"{Name} Lv     : {Level}");
            Console.WriteLine($"공격력 : {Att} (+{EAtt})");
            Console.WriteLine($"방어력 : {Def} (+{EDef})");
            Console.WriteLine($"체 력  : {NHp}");
            Console.WriteLine($"마 나  : {NMp}");
        }
        public void AddItem(Item item)
        {
            _inventory.Rooting(item);
        }
        
        public int Level { get; private set; }
        public string Name { get; private set; }
        public int Att { get; private set; }
        public int EAtt { get; private set; }
        public int Def { get; private set; }
        public int EDef { get; private set; }
        public int Hp { get; private set; }
        public int NHp { get; private set; }
        public int EHp { get; private set; }
        public int Mp { get; private set; }
        public int NMp { get; private set; }
        public int EMp { get; private set; }
        public int Gold { get; private set; }
        public int Exp { get; private set; }
        public int MaxExp { get; private set; }

        public Gadget _gadget = new Gadget();
        public Inventory _inventory = new Inventory();
    }
}
