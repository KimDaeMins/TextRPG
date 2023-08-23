// See https://aka.ms/new-console-template for more information

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            player.Init("김대민");

            LobbyScene lobbyScene = new LobbyScene();
            lobbyScene.InitLobby();
            lobbyScene.ShowLobby(player);
            
        }

    }
}