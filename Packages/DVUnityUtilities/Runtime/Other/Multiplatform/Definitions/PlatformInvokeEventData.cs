using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Multiplatform.Definitions
{
    [Serializable]
    internal class PlatformInvokeEventData
    {
        public List<RuntimePlatform> Platforms = new();
        public UnityEvent Action;
    }
}