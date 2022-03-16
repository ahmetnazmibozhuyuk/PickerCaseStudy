using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Picker.Interactable;

namespace Picker.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private ParticleSystem endLevelWinParticle;
        [SerializeField] private ObjectScript spherePrefab;
        [SerializeField] private ObjectScript capsulePrefab;
        [SerializeField] private ObjectScript boxPrefab;
        [SerializeField] private ObjectScript cylinderPrefab;

        private Queue<ParticleSystem> _particleQueue = new Queue<ParticleSystem>();
        private Queue<ObjectScript> _spherePrefabQueue = new Queue<ObjectScript>();
        private Queue<ObjectScript> _capsulePrefabQueue = new Queue<ObjectScript>();
        private Queue<ObjectScript> _boxPrefabQueue = new Queue<ObjectScript>();
        private Queue<ObjectScript> _cylinderPrefabQueue = new Queue<ObjectScript>();

        private ObjectPool<ParticleSystem> _particlePool;
        private ObjectPool<ObjectScript> _spherePrefabPool;
        private ObjectPool<ObjectScript> _capsulePrefabPool;
        private ObjectPool<ObjectScript> _boxPrefabPool;
        private ObjectPool<ObjectScript> _cylinderPrefabPool;

        private void Start()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            InitializeParticlePool();
            InitializeSpherePrefabPool();
            InitializeCapsulePrefabPool();
            InitializeBoxPrefabPool();
            InitializeCylinderPrefabPool();
        }
        #region Pool Initializers
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
        private void InitializeSpherePrefabPool()
        {
            _spherePrefabPool = new ObjectPool<ObjectScript>(() =>
            {
                return Instantiate(spherePrefab);
            }, spherePrefab =>
            {
                spherePrefab.gameObject.SetActive(true);
            }, spherePrefab =>
            {
                spherePrefab.gameObject.SetActive(false);
            }, spherePrefab =>
            {
                Destroy(spherePrefab.gameObject);
            });
        }
        private void InitializeCapsulePrefabPool()
        {
            _capsulePrefabPool = new ObjectPool<ObjectScript>(() =>
            {
                return Instantiate(capsulePrefab);
            }, capsulePrefab =>
            {
                capsulePrefab.gameObject.SetActive(true);
            }, capsulePrefab =>
            {
                capsulePrefab.gameObject.SetActive(false);
            }, capsulePrefab =>
            {
                Destroy(capsulePrefab.gameObject);
            });
        }
        private void InitializeBoxPrefabPool()
        {
            _boxPrefabPool = new ObjectPool<ObjectScript>(() =>
            {
                return Instantiate(boxPrefab);
            }, boxPrefab =>
            {
                boxPrefab.gameObject.SetActive(true);
            }, boxPrefab =>
            {
                boxPrefab.gameObject.SetActive(false);
            }, boxPrefab =>
            {
                Destroy(boxPrefab.gameObject);
            });
        }
        private void InitializeCylinderPrefabPool()
        {
            _cylinderPrefabPool = new ObjectPool<ObjectScript>(() =>
            {
                return Instantiate(cylinderPrefab);
            }, cylinderPrefab =>
            {
                cylinderPrefab.gameObject.SetActive(true);
            }, cylinderPrefab =>
            {
                cylinderPrefab.gameObject.SetActive(false);
            }, cylinderPrefab =>
            {
                Destroy(cylinderPrefab.gameObject);
            });
        }
        #endregion
        #region Pool Get and Release Methods
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

        public void PrefabSpawn(PrefabToSpawn prefab, Vector3 spawnLocation)
        {
            switch (prefab)
            {
                case PrefabToSpawn.capsule:
                    var capsule = _capsulePrefabPool.Get();
                    capsule.transform.position = spawnLocation;
                    _capsulePrefabQueue.Enqueue(capsule);
                    break;
                case PrefabToSpawn.box:
                    var box = _boxPrefabPool.Get();
                    box.transform.position = spawnLocation;
                    _boxPrefabQueue.Enqueue(box);
                    break;
                case PrefabToSpawn.sphere:
                    var sphere = _spherePrefabPool.Get();
                    sphere.transform.position = spawnLocation;
                    _spherePrefabQueue.Enqueue(sphere);
                    break;
            }
        }
        public void ReleaseAllPrefabPools()
        {
            for(int i = 0; i < _capsulePrefabQueue.Count; i++)
            {
                _capsulePrefabPool.Release(_capsulePrefabQueue.Peek());
                _capsulePrefabQueue.Dequeue();
            }
            for (int i = 0; i < _boxPrefabQueue.Count; i++)
            {
                _boxPrefabPool.Release(_boxPrefabQueue.Peek());
                _boxPrefabQueue.Dequeue();
            }
            for (int i = 0; i < _spherePrefabQueue.Count; i++)
            {
                _spherePrefabPool.Release(_spherePrefabQueue.Peek());
                _spherePrefabQueue.Dequeue();
            }
        }
        #endregion
    }
}
