using System;
using UnityEngine;

namespace Assets.Scripts.Core.Advertising.Types
{
    internal class IronSourceAdSystem : MonoBehaviour, IAdSystem
    {
        [SerializeField] private string _androidAppKey = "1dec1c9d5";
        [SerializeField] private string _iphoneAppKey = "1dec1c9d5";

#if UNITY_ANDROID
        private string appKey => _androidAppKey;
#elif UNITY_IPHONE
        private string appKey =>_iphoneAppKey;
#else
        private string appKey = "unexpected_platform";
#endif

        private void Awake()
        {
            Debug.Log("unity-script: IronSource.Agent.validateIntegration");
            IronSource.Agent.validateIntegration();

            Debug.Log("unity-script: unity version" + IronSource.unityVersion());

            Debug.Log("unity-script: IronSource.Agent.init");
            IronSource.Agent.init(appKey);
        }

        public void ShowRewardVideo()
        {
            try
            {
                IronSource.Agent.showRewardedVideo();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}