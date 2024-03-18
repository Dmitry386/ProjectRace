using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.Optimization
{
    internal class MonoStaticBatchingUtility : MonoBehaviour
    {
        private void Start()
        {
            var gos = Util.GetAllChilds(transform).Select(x=>x.gameObject).ToArray();
            StaticBatchingUtility.Combine(gos, gameObject);
        }
    }
}