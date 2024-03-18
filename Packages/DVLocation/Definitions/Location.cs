using DVUnityUtilities;
using UnityEngine;

namespace Assets.Scripts.World.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private string _customName = string.Empty;

        private void Awake()
        {
            if (string.IsNullOrEmpty(_customName))
            {
                _customName = StringUtils.FromSceneNameToObjectName(name);
            }
        }

        private void OnDestroy()
        {
            GameObject.Destroy(gameObject);
        }

        public override string ToString()
        {
            return _customName;
        }
    }
}