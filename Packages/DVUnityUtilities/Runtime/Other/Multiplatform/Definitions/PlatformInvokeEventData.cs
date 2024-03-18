using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Multiplatform.Definitions
{
    internal class PlatformInvokeEventData
    {
        public List<RuntimePlatform> Platforms = new();
        public UnityEvent Action;
    }
}