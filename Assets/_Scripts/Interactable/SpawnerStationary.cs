using UnityEngine;
using Picker.Managers;

namespace Picker.Interactable
{
    public class SpawnerStationary : Spawner
    {
        [SerializeField] private PrefabToSpawn objectToSpawn;

        [SerializeField] [Range(3, 12)] private int objectSpawnAmount;

        private float _scatterAmount = 0.2f;
        public override void ActivateSpawner()
        {
            Burst();
        }
        private void Burst()
        {
            for (int i = 0; i < objectSpawnAmount; i++)
            {    
                PoolManager.Instance.PrefabSpawn(objectToSpawn, new Vector3(
                    transform.position.x + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.y + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.z));
            }
            Destroy(gameObject);
        }
    }
}

