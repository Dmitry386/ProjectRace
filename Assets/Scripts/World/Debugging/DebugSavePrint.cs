using Assets.Scripts.Core.Saving;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.World.Debugging
{
    internal class DebugSavePrint : MonoBehaviour
    {
        [Inject] private SaveSystem _saveSystem;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (_saveSystem.Load(out var save))
                {
                    Debug.Log(JsonUtility.ToJson(save, true));
                }
            }
        }
    }
}