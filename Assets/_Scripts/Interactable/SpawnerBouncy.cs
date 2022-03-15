using UnityEngine;
//public interface ISpawnerActivate
//{
//    public void ActivateSpawner();
//}
namespace Picker.Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpawnerBouncy : Spawner
    {
        [SerializeField] private GameObject objectToSpawn;

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
                Instantiate(objectToSpawn, new Vector3(
                    transform.position.x + Random.Range(-5, 5) * _scatterAmount,
                    transform.position.y + Random.Range(-5, 5) * _scatterAmount,
                    transform.position.z),
                    transform.rotation, transform.parent);
            }
            Destroy(gameObject);
        }
    }
}

