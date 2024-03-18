using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.NetworkControllers;
using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    internal class Bootstrap : MonoInstaller
    {
        public override void InstallBindings()
        {
            ContainerBindInstance<SaveSystem>();
            ContainerBindInstance<BuySystem>();
            ContainerBindInstance<VehicleSwitcher>();
            ContainerBindInstance<TuningControl>();

            Container.BindInstance<INetworkControl>(GameObject.FindAnyObjectByType<PhotonNetworkControl>());
        }

        private void ContainerBindInstance<T>() where T : Object
        {
            Container.BindInstance<T>(GameObject.FindAnyObjectByType<T>());
        }
    }
}