using UnityEngine;

namespace Picker.Interactable
{
    /* 
     * This class can be used by any class driven from Spawner abstract class. Simply attach this class to an empty object,
     * use collider as a trigger and attach the game object as child of the Spawner game object.
    */
    [RequireComponent(typeof(Collider))]
    public class SpawnerTrigger : MonoBehaviour
    {
        private Spawner _spawner;

        private void Awake()
        {
            _spawner = GetComponentInParent<Spawner>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _spawner.ActivateSpawner();
        }
    }
}

