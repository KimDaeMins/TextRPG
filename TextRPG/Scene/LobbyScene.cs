using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class LobbyScene
    {
        private InventoryScene _inventoryScene = new InventoryScene();
        private GadgetScene _gadgetScene = new GadgetScene();
        private ShopScene _shopScene = new ShopScene();
        private RestScene _restScene = new RestScene();
        private DungeonScene _dungeonScene = new DungeonScene();
        public void InitLobby()
        {
            _shopScene.Init();
        }
        public void ShowLobby(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 장비창");
                Console.WriteLine("4. 상점");
                Console.WriteLine("5. 던전입장");
                Console.WriteLine("6. 휴식소");
                Console.WriteLine("0. 게임 종료");

                int select = 0;
                Console.WriteLine("---------------------------");
                Console.Write("선택 : ");
                int.TryParse(Console.ReadLine(), out select);

                switch (select)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        player.ShowStatus();
                        Console.WriteLine("0. 나가기");
                        int choice = 0;
                        int.TryParse(Console.ReadLine(), out choice);
                        break;
                    case 2:
                        _inventoryScene.ShowInven(player._inventory, player._gadget);
                        break;
                    case 3:
                        _gadgetScene.ShowGadget(player._inventory, player._gadget);
                        break;
                    case 4:
                        _shopScene.ShowShop(player);
                        break;
                    case 5:
                        _dungeonScene.ShowStage(player);
                        break;
                    case 6:
                        _restScene.ShowResting(player);
                        break;
                }
            }
        }
    }
}
