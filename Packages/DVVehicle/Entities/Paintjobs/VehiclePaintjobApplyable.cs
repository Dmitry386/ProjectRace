using DVUnityUtilities;
using Packages.DVVehicle.Entities.Vehicles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Packages.DVVehicle.Entities.Paintjobs
{
    [RequireComponent(typeof(VehicleEntity))]
    public class VehiclePaintjobApplyable : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public string PlaceName = string.Empty;
        public List<PaintjobData> AvailablePaintjobs = new();


        private PaintjobData _actualPaintjob;
        private Material[] _noPaintjob;

        private void Awake()
        {
            _noPaintjob = _meshRenderer.sharedMaterials;
        }

        public void SetPaintjob(PaintjobData paint)
        {
            _actualPaintjob = paint;

            if (paint == null)
            {
                RestoreDefaultVisualizationPaintjob();
            }
            else
            {
                VisualizePaintjob();
            }
        }

        public bool IsHavePaintjob(out PaintjobData paintjob)
        {
            paintjob = _actualPaintjob;
            return paintjob != null;
        }

        private void VisualizePaintjob()
        {
            var mats = _actualPaintjob.Materials;
            var new_mats = _noPaintjob.ToArray();

            for (int i = 0; i < mats.Count; i++)
            {
                if (mats[i])
                {
                    new_mats[i] = mats[i];
                }
            }

            _meshRenderer.sharedMaterials = new_mats;
        }

        private void RestoreDefaultVisualizationPaintjob()
        {
            _meshRenderer.sharedMaterials = _noPaintjob;
        }

        public bool IsCanBeApplyedSecured(PaintjobData data)
        {
            return AvailablePaintjobs.FirstOrDefault(x => x.Name == data.Name) != null
                && data.Materials.Count == _meshRenderer.sharedMaterials.Length;
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(PlaceName))
            {
                PlaceName = StringUtils.FromSceneNameToObjectName(name);
            }
        }
    }
}