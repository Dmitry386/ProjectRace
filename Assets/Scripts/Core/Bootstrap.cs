using Assets.Scripts.Core.Other;
using Assets.Scripts.Core.Saving;
using Assets.Scripts.Entities.Buyable;
using Assets.Scripts.UI.Garage.TuningMenu;
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
        }

        private void ContainerBindInstance<T>() where T : Object
        {
            Container.BindInstance<T>(GameObject.FindAnyObjectByType<T>());
        }
    }
}