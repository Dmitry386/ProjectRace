using Assets.Scripts.Core.Other.DriftPoints;
using DVUnityUtilities.Other.Cooldowners;
using Packages.DVMessageBoxes.Source.Dialogs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Garage.VehicleDriftUI
{
    internal class VehicleDriftUI : MonoBehaviour
    {
        [SerializeField] private float _showTime = 2f;
        [SerializeField] private RectTransform _objectToHide;
        [SerializeField] private TextMeshProUGUI _textMesh;

        [Inject] private VehicleDriftGlobalController _controller;

        private Cooldown _cooldown = new();

        private void Awake()
        {
            _controller.OnUpdatePreviewDriftValue += OnUpdatePreviewDriftValue;
            _objectToHide.gameObject.SetActive(false);
        }

        private void OnUpdatePreviewDriftValue(DriftPointsEventArgs args)
        {
            _textMesh.text = $"DRIFT POINTS: {(int)args.Points}";
            StartWaiting();
        }

        private void OnRequestDefaultAward(float money)
        {
            Debug.Log("Request DEFAULT AWARD"); // todo: todo
        }

        private void OnRequestAwardWithAd(float money)
        {
            Debug.Log("Request AD AWARD"); // todo: todo
        }

        private void StartWaiting()
        {
            _objectToHide.gameObject.SetActive(true);
            _cooldown.UpdateLastUseTime();
        }

        private void Update()
        {
            if (_cooldown.IsTimeOver(_showTime))
            {
                _objectToHide.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _controller.OnUpdatePreviewDriftValue -= OnUpdatePreviewDriftValue;
        }
    }
}