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
        Sphere = 0,
        Capsule = 1,
        Box = 2
    }
}