using UnityEngine;

namespace DVUnityUtilities.Other.Animations.Simple
{
    internal class DVA_AddRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _addRotation;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Space _space = Space.Self;
        [SerializeField] private float _maxOffset = 0.1f;
        [SerializeField] private bool _disableOnFinish = true;

        private Transform _cachedT;
        private Quaternion _target;

        private void OnEnable()
        {
            RecalculateRotationTarget();
        }

        private void Update()
        {
            Rotation = Quaternion.RotateTowards(Rotation, _target, Time.deltaTime * _speed);

            if (Quaternion.Angle(Rotation, _target) <= _maxOffset)
            {
                //Debug.Log($"{Rotation} - {_target} => {Quaternion.Angle(Rotation, _target)}");
                if (_disableOnFinish)
                {
                    enabled = false;
                }
                else
                {
                    RecalculateRotationTarget();
                }
            }
        }

        private void RecalculateRotationTarget()
        {
            _cachedT = transform;
            _target = Rotation * Quaternion.Euler(_addRotation);
        }

        private Quaternion Rotation
        {
            set
            {
                if (_space == Space.Self) _cachedT.localRotation = value;
                else _cachedT.rotation = value;
            }
            get
            {

                if (_space == Space.Self) return _cachedT.localRotation;
                else return _cachedT.rotation;
            }
        }
    }
}