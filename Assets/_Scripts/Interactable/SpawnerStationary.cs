using UnityEngine;

namespace Picker.Interactable
{
    public class SpawnerStationary : Spawner
    {
        public override GameObject ObjectToSpawn { get => objectToSpawn; set => objectToSpawn = value; }
        [SerializeField] private GameObject objectToSpawn;

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
                Instantiate(ObjectToSpawn, new Vector3(transform.position.x + Random.Range(-5, 5) * _scatterAmount, transform.position.y + Random.Range(-5, 5) * _scatterAmount,
                    transform.position.z),
                    transform.rotation, transform.parent);
            }
            Destroy(gameObject);
        }
    }
}

