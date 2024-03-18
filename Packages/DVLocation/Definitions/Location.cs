using DVUnityUtilities;
using UnityEngine;

namespace Assets.Scripts.World.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private string _customName = string.Empty;

        private void OnDestroy()
        {
            GameObject.Destroy(gameObject);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_customName))
            {
                return StringUtils.FromSceneNameToObjectName(name);
            }
            else
            {
                return _customName;
            }
        }
    }
}