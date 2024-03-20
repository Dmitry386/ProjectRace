using Assets.Scripts.World.Locations;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Garage.TextDisplays
{
    internal class LocationTimerUI : MonoBehaviour
    {
        [SerializeField] private string _printFormat = "TIME REMAINING: {0}";
        [SerializeField] private LocationTimer _timer;
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        private void Update()
        {
            if (_timer)
            {
                UpdateValue(_timer.GetRemainingTime());
            }
            else
            {
                UpdateValue(0);
            }
        }

        private void UpdateValue(float v)
        {
            _textMeshPro.text = string.Format(_printFormat,((int)v).ToString());
        }
    }
}