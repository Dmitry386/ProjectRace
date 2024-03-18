using UnityEngine;

namespace DVUnityUtilities.Other.Pivots
{
    public class CustomTransformPivot
    {
        public Transform Transform;
        public Transform PivotTransform;

        private Vector3 _worldPivotPosition => PivotTransform.position;
        private Vector3 _transformPositionOnSetPivotTime => Transform.position;

        private Vector3 PivotDirectionFromCenter => _worldPivotPosition - _transformPositionOnSetPivotTime;
        private Vector3 PivotDirectionToCenter => _transformPositionOnSetPivotTime - _worldPivotPosition;

        public Vector3 PivotPosition
        {
            get
            {
                return Transform.position + PivotDirectionFromCenter;
            }
            set
            {
                Transform.position = value + PivotDirectionToCenter;
            }
        }
        public Vector3 PivotRotation
        {
            get
            {
                return PivotTransform.eulerAngles;
            }
            set
            {
                Vector3 offset = value - Transform.eulerAngles;
                Transform.RotateAround(PivotPosition, Vector3.up, offset.y);
                Transform.RotateAround(PivotPosition, Vector3.right, offset.x);
                Transform.RotateAround(PivotPosition, Vector3.forward, offset.z);
            }
        }

        public CustomTransformPivot(Transform transform, Transform pivot)
        {
            Transform = transform;
            PivotTransform = pivot;
        }

    }
}