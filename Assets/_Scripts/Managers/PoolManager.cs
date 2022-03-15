using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Picker.Interactable;

namespace Picker.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        private ObjectPool<ParticleSystem> _particlePool;
        private ObjectPool<ObjectScript> _spherePrefabPool;

        [SerializeField] private ParticleSystem endLevelWinParticle;

        [SerializeField] private ObjectScript spherePrefab;


        private Queue<ParticleSystem> _particleQueue = new Queue<ParticleSystem>();

        private Queue<ParticleSystem> _spherePrefabQueue = new Queue<ParticleSystem>();


        private void Start()
        {
            InitializeParticlePool();

        }

        private void InitializeParticlePool()
        {
            _particlePool = new ObjectPool<ParticleSystem>(() =>
            {
                return Instantiate(endLevelWinParticle);
            }, endLevelWinParticle =>
            {
                endLevelWinParticle.gameObject.SetActive(true);
            }, endLevelWinParticle =>
            {
                endLevelWinParticle.gameObject.SetActive(false);
            }, endLevelWinParticle =>
            {
                Destroy(endLevelWinParticle.gameObject);
            });
        }
        public void SpawnParticle(Vector3 particleLocation)
        {
            StartCoroutine(Co_ParticleSpawner(particleLocation));
        }
        private IEnumerator Co_ParticleSpawner(Vector3 particleLocation)
        {
            var particle = _particlePool.Get();
            particle.transform.position = particleLocation;
            _particleQueue.Enqueue(particle);
            yield return new WaitForSeconds(1);
            _particlePool.Release(_particleQueue.Peek());
            _particleQueue.Dequeue();
        }

        public void PrefabSpawn(PrefabToSpawn prefab)
        {

        }
    }
}
