using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class RestScene
    {
        public void ShowResting(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"현재 체력 :  {player.NHp} / {player.Hp + player.EHp}");
                Console.WriteLine($"500 G 를 내면 체력을 100 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 0)
                    return;

                if (choice == 1)
                { 
                    if (player.Gold >= 500)
                    {
                        if(player.NHp == player.Hp + player.EHp)
                            Console.WriteLine("풀피임");
                        else
                        {
                            Console.WriteLine($"Hp {player.HpUp(100)} 회복!");
                            player.AddGold(-500);
                        }
                    }
                    else
                    {
                        Console.WriteLine("돈부족");
                    }
                }
                Console.WriteLine("아무키나 눌러서 나가기");
                Console.ReadLine();
            }
        }
    }
}
