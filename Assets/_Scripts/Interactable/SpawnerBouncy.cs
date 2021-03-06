using UnityEngine;
using Picker.Managers;

namespace Picker.Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpawnerBouncy : Spawner
    {
        [Tooltip("Select which prefab to spawn when this object bursts.")]
        [SerializeField] private PrefabToSpawn objectToSpawn;

        [SerializeField] [Range(3, 12)] private int objectSpawnAmount;

        private float _scatterAmount = 0.1f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public override void ActivateSpawner()
        {
            _rigidbody.isKinematic = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            Burst();
        }
        private void Burst()
        {
            for (int i = 0; i < objectSpawnAmount; i++)
            {
                PoolManager.instance.PrefabSpawn(objectToSpawn, new Vector3(
                    transform.position.x + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.y + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.z));
            }
            Destroy(gameObject);
        }
    }
}

