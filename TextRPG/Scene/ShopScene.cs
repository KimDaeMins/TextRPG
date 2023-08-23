using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ShopScene
    {
        private List<Item> _itemList = new List<Item>();
        public void Init()
        {
            _itemList.Add(new Wear(WearTypes.Body, "수련자 갑옷", 2, 0, 0, 7, 1500));
            _itemList.Add(new Wear(WearTypes.Head, "수련자 모자", 1, 0, 0, 5, 1500));
            _itemList.Add(new Wear(WearTypes.Body, "스파르타의 갑옷", 3, 0, 0, 15, 3500));
            _itemList.Add(new OneHandSword("한손 검", 3, 15, 0,  1000));
            _itemList.Add(new Shield("스파르타의 방패", 3, 0, 15, 2000));
        }
        public void ShowShop(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();

                Console.WriteLine("1.구매하기");
                Console.WriteLine("2.판매하기");
                Console.WriteLine("0.나가기");

                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 0)
                    return;
                if (choice == 1)
                {
                    ShowBuy(player);
                }
                else if (choice == 2)
                {
                    ShowSell(player);
                }
            }
        }

        private void ShowSell(Player player)
        {
            Console.Clear();
            Console.WriteLine("아이템을 파는곳입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("1.장비 판매");
            Console.WriteLine("2.소모품 판매");
            Console.WriteLine("3.기타 판매");
            Console.WriteLine("0.나가기");

            int choice = 0;
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 0)
                return;

            switch (choice)
            {
                case 1:
                    ShowSell(player, ItemKinds.Head, ItemKinds.Weapon);
                    break;
                case 2:
                    ShowSell(player, ItemKinds.Consumables, ItemKinds.Consumables);
                    break;
                case 3:
                    ShowSell(player, ItemKinds.Others, ItemKinds.Others);
                    break;
            }
        }
        private int Choice(int count)
        {
            while (true)
            {
                int select;
                Console.WriteLine("---------------------------");
                Console.Write(">> ");
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
        private void ShowSell(Player player, ItemKinds startKind, ItemKinds endKind = ItemKinds.End)
        {
            while (true)
            {
                Inventory inven = player._inventory;
                if (endKind == ItemKinds.End)
                {
                    endKind = startKind;
                }

                Console.Clear();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();


                Console.WriteLine("[아이템 목록]");
                int count = inven.ShowSell(startKind, endKind);

                Console.WriteLine("0.뒤로가기");
                int select = Choice(count);
                if (select == 0)
                {
                    return;
                }

                int j = (int)startKind;
                while (select - inven.GetItemCount(j) > 0)
                {
                    select -= inven.GetItemCount(j);
                    j++;
                }
                select -= 1;

                SellItem(player, j, select);
                Console.ReadLine();
            }
        }

        private void SellItem(Player player, int kind, int index)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[아이템 정보]");
                Item item = player._inventory.GetItem(kind, index);
                item.ShowDetailSell();

                Console.WriteLine("1.판매하기 0.뒤로가기");
                Console.Write(">> ");
                int select = 0;
                int.TryParse(Console.ReadLine(), out select);
                if (select == 0)
                {
                    return;
                }
                int gold = player.Sell(kind, index);
                if (gold != -1)
                {
                    player.AddGold(gold);
                    return;
                }
            }
        }
        private void ShowBuy(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("아이템을 사는곳입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();

                Console.WriteLine("[아이템 목록]");
                int count = 1;
                foreach (var item in _itemList)
                {
                    item.ShowDetailBuy(count++);
                }
                Console.WriteLine("0. 나가기");

                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 0)
                    return;
                if (choice >= count)
                {
                    Console.WriteLine("잘못입력하셨습니다. 아무키나 눌러 다시선택");
                    Console.ReadLine();
                    continue;
                }
                BuyItem(player, choice - 1);
                Console.ReadLine();
            }
        }

        private void BuyItem(Player player, int index)
        {
            if(player.Gold >= _itemList[index].Gold)
            {
                player.AddItem(_itemList[index]);
                Console.WriteLine("구매를 완료했습니다");
                player.AddGold(-_itemList[index].Gold);
                _itemList.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
        }
    }
}
