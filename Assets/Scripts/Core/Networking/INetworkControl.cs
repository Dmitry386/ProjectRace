using Assets.Scripts.Core.Networking.Definitions;

namespace Assets.Scripts.Core.Networking
{
    public interface INetworkControl
    {
        public void Connect(string address);
        public void Disconnect();

        public void StartHost();
        public void StopHost();
        public void Kick(string playerName);

        public NetworkStatus GetNetworkStatus();
        public string GetCurrentAddress();

        public bool IsHavePlayer(string name, out NetworkPlayerInfo player);
        public NetworkPlayerInfo[] GetPlayers();
        public int GetCurrentPlayerPing();
    }
}