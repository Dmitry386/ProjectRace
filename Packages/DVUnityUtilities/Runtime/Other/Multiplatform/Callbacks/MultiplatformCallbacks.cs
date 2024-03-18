using DVUnityUtilities.Other.Multiplatform.Definitions;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.Multiplatform.Callbacks
{
    internal class MultiplatformCallbacks : MonoBehaviour
    {
        [SerializeField] private bool _invokeOnAwake = true;
        [SerializeField] private List<PlatformInvokeEventData> _platformInvokeData = new();

        private void Awake()
        {
            if (_invokeOnAwake)
            {
                InvokeEvents();
            }
        }

        private void Start()
        {
            if (!_invokeOnAwake)
            {
                InvokeEvents();
            }
        }

        private void InvokeEvents()
        {
            foreach (var data in _platformInvokeData)
            {
                if (data.Platforms.Contains(Application.platform))
                {
                    try
                    {
                        data.Action.Invoke();
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
        }
    }
}