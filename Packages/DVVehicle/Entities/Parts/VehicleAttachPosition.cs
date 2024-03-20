using DVUnityUtilities;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Parts
{
    public class VehicleAttachPosition : MonoBehaviour
    {
        [SerializeField] private string _attachPositionName = string.Empty;

        private Transform _currentObject;
        private VehicleAttachObject _attachedObject;

        public void SetObject(VehicleAttachObject o)
        {
            RemoveObject();

            if (o != null && o.Prefab)
            {
                var copy = GameObject.Instantiate(o.Prefab);

                copy.SetParent(transform, true);
                copy.localPosition = Vector3.zero;
                copy.localRotation = Quaternion.identity;

                _currentObject = copy;
                _attachedObject = o;
            }
        }

        public void RemoveObject()
        {
            if (_currentObject)
            {
                GameObject.Destroy(_currentObject.gameObject);
                _currentObject = null;
            }

            _attachedObject = null;
        }

        public bool IsAttachedObject(out VehicleAttachObject data, out Transform obj)
        {
            data = _attachedObject;
            obj = _currentObject;
            return obj != null && data != null;
        }

        public string GetAttachPositionName()
        {
            return _attachPositionName;
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_attachPositionName))
            {
                _attachPositionName = StringUtils.FromSceneNameToObjectName(name);
            }
        }
    }
}