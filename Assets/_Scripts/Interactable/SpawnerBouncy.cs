using UnityEngine;

namespace Picker.Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpawnerBouncy : Spawner
    {
        public override GameObject ObjectToSpawn { get => objectToSpawn; set => objectToSpawn = value; }
        [SerializeField] private GameObject objectToSpawn;

        [SerializeField] [Range(3, 12)] private int objectSpawnAmount;

        private float _scatterAmount = 0.1f;

        private Rigidbody _rigidbody;

        [SerializeField] private ObjectScript objectScript;

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
                Instantiate(ObjectToSpawn, new Vector3(
                    transform.position.x + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.y + Random.Range(-ScatterRange, ScatterRange) * _scatterAmount,
                    transform.position.z),
                    transform.rotation, transform.parent);
            }
            Destroy(gameObject);
        }
    }
}

