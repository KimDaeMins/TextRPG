using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class DungeonScene
    {
        public int ClearCount = 0;
        public void ShowStage(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 입장");
                Console.WriteLine();
                Console.WriteLine("1. 쉬운 던전  | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전  | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전| 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">> ");

                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 0)
                    return;

                //일단 구성에 따라가자
                switch (choice)
                {
                    case 1:
                        BeginnerDungeon(player);
                        break;
                    case 2:
                        NormalDungeon(player);
                        break;
                    case 3:
                        HardDungeon(player);
                        break;
                }
            }
        }

        private void Result(Player player, string dungeonName, bool isWin, int downHp = 0, int resultGold = 0)
        {
            int playerHp = player.NHp;
            int playerNHp = player.HpDown(downHp);

            if (playerNHp > 0 && isWin)
            {
                ++ClearCount;
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dungeonName}을 클리어 하였습니다");

                if(ClearCount > player.Level)
                {
                    Console.WriteLine("레벨업!");
                    ClearCount = 0;
                    player.LevelUp();
                }
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {playerHp} -> {playerNHp}");
                player.AddGold(resultGold);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("졌다!");
                if (playerNHp <= 0)
                {
                    Console.WriteLine("죽었다!");
                    Console.WriteLine("부활 및 체력 초기화");
                    Console.WriteLine("가진돈 전부 감소!");

                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {playerHp} -> {player.NHp}");
                    player.SetGold(0);
                }
                else
                {
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {playerHp} -> {playerNHp}");
                    player.AddGold(-resultGold);
                }
            }
            Console.WriteLine("아무키나 눌러서 나가기");
            Console.ReadLine();
            
        }
        private void BeginnerDungeon(Player player)
        {
            int resultGold = 1000;
            int needDef = 5;
            int playerDef = player.Def + player.EDef;
            int defGap = needDef - playerDef;
            Random random = new Random();

            if (needDef > playerDef && random.Next(0, 100) < 40)
            {
                    Result(player, "초급 던전", false, player.NHp / 2 + 10, resultGold / 3);
            }
            else
            {
                Result(player, "초급 던전", true, random.Next(20 + defGap, 35 + defGap),
                    resultGold + (int)(resultGold / 100 * (player.Att + player.EAtt) * (1.0f + random.NextSingle())));
            }
        }
        private void NormalDungeon(Player player)
        {
            int resultGold = 1700;
            int needDef = 11;
            int playerDef = player.Def + player.EDef;
            int defGap = needDef - playerDef;
            Random random = new Random();

            if (needDef > playerDef && random.Next(0, 100) < 40)
            {
                Result(player, "일반 던전", false, player.NHp / 2 + 10, resultGold / 3);
            }
            else
            {
                Result(player, "일반 던전", true, random.Next(20 + defGap, 35 + defGap),
                    resultGold + (int)(resultGold / 100 * (player.Att + player.EAtt) * (1.0f + random.NextSingle())));
            }
        }
        private void HardDungeon(Player player)
        {
            int resultGold = 2500;
            int needDef = 17;
            int playerDef = player.Def + player.EDef;
            int defGap = needDef - playerDef;
            Random random = new Random();

            if (needDef > playerDef && random.Next(0, 100) < 40)
            {
                Result(player, "어려움 던전", false, player.NHp / 2 + 10, resultGold / 3);
            }
            else
            {
                Result(player, "어려움 던전", true, random.Next(20 + defGap, 35 + defGap),
                    resultGold + (int)(resultGold / 100 * (player.Att + player.EAtt) * (1.0f + random.NextSingle())));
            }
        }
    }
}
