using UnityEngine;

namespace Picker.Interactable
{
    public abstract class Spawner : MonoBehaviour
    {
        public const int ScatterRange = 5;
        public abstract void ActivateSpawner();
    }
    public enum PrefabToSpawn
    {
        sphere = 0,
        capsule = 1,
        box = 2
    }
}