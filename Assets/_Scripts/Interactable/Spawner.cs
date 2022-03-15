using UnityEngine;

namespace Picker.Interactable
{
    public abstract class Spawner : MonoBehaviour
    {
        public abstract GameObject ObjectToSpawn { get; set; }
        public abstract void ActivateSpawner();
    }
}