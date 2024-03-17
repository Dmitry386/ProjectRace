using Assets.Scripts.Core.Networking.Definitions;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Networking
{
    public interface INetworkControl
    {
        public void Connect(string address);
        public void Disconnect();

        public void StartHost();
        public void StopHost();
        public void Kick(string playerName);

        public bool IsHavePlayer(string name, out NetworkPlayerInfo player);
        public NetworkPlayerInfo[] GetPlayers();
    }
}