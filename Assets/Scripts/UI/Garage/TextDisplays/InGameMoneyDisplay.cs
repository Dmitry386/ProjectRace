using Assets.Scripts.Core.Saving;
using DVUnityUtilities.Other.Coroutines;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.TextDisplays
{
    internal class InGameMoneyDisplay : MonoBehaviour
    {
        [SerializeField] private string _stringFormat = "{0} $";
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private float _updateInterval = 0.5f;

        [Inject] private SaveSystem _saveSystem;

        private void OnEnable()
        {
            new CoroutineTimer(this, _updateInterval, true).Start().OnTick += OnTick;
            OnTick(null);
        }

        private void OnTick(CoroutineTimer obj)
        {
            if (_saveSystem.Load(out var saveData))
            {
                _textMesh.text = string.Format(_stringFormat, saveData.InGameMoney);
            }
        }
    }
}