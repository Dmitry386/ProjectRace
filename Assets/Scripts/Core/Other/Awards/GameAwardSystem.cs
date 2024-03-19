using Assets.Scripts.Core.Other.DriftPoints;
using Assets.Scripts.Core.Saving;
using Packages.DVMessageBoxes.Source.Dialogs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Other.Awards
{
    internal class GameAwardSystem : MonoBehaviour
    {
        [SerializeField] private float _advertisingAwardMultiplier = 2f;

        [Inject] private SaveSystem _saveSystem;
        [Inject] private VehicleDriftGlobalController _controller;

        private void Awake()
        {
            _controller.OnMoneyPerDriftPointRequsted += OnMoneyPerDriftPointRequested;
        }

        private void OnMoneyPerDriftPointRequested(DriftPointsEventArgs args)
        {
            ShowAwardRequest($"You have earned {(int)args.Points} drift points.\nFor this merit you are entitled to a reward in the amount of: {(int)args.Money} $", (int)args.Money);
        }

        public void ShowAwardRequest(string text, int money)
        {
            var dial = new MessageDialog("Reward", text, "Ok", "X2", null);
            dial.OnResponse += (e) =>
            {
                if (e.DialogButton == 0)
                {
                    GiveAwardWithoutAd(money);
                }
                if (e.DialogButton == 1)
                {
                    GiveAwardWithAd((int)(money * _advertisingAwardMultiplier));
                }
            };
            dial.Show();
        }

        public void GiveAwardWithAd(int money)
        {
            if (_saveSystem.Load(out var save))
            {
                // todo: ADDDDDDDDDDDDDD    
            }
        }

        public void GiveAwardWithoutAd(int money)
        {
            if (_saveSystem.Load(out var save))
            {
                save.TryGiveMoney(money);
            }
        }

        private void OnDestroy()
        {
            _controller.OnMoneyPerDriftPointRequsted -= OnMoneyPerDriftPointRequested;
        }
    }
}