using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Picker.Interactable;

namespace Picker.Managers
{
    /// <summary>
    ///  Summon and remove objects from this class instead of instantiating and destroying them.
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        /*
         * The main reason for me to create this project using Unity version 2021.2.15f1 is the new native object pooling. I had a simple pooling script
         * that was ready to use but I thought this case study would be a great opportunity to switch.
         *
         * This class contains any object that needs to be pooled and can be extended with ease. It was made singleton so its exposed
         * methods can be used from any script that needs it.
         */
        [SerializeField] private ParticleSystem endLevelWinParticle;
        [SerializeField] private ObjectScript spherePrefab;
        [SerializeField] private ObjectScript capsulePrefab;
        [SerializeField] private ObjectScript boxPrefab;

        private Queue<ParticleSystem> _particleQueue = new();
        private Queue<ObjectScript> _spherePrefabQueue = new();
        private Queue<ObjectScript> _capsulePrefabQueue = new();
        private Queue<ObjectScript> _boxPrefabQueue = new();

        private ObjectPool<ParticleSystem> _particlePool;
        private ObjectPool<ObjectScript> _spherePrefabPool;
        private ObjectPool<ObjectScript> _capsulePrefabPool;
        private ObjectPool<ObjectScript> _boxPrefabPool;

        private void Start()
        {
            InitializePools();
        }
        #region Pool Initializers
        private void InitializePools()
        {
            _particlePool = InitializeParticlePool(endLevelWinParticle);

            _spherePrefabPool = InitializeObjectScript(spherePrefab);
            _capsulePrefabPool = InitializeObjectScript(capsulePrefab);
            _boxPrefabPool = InitializeObjectScript(boxPrefab);
        }
        private ObjectPool<ObjectScript> InitializeObjectScript(ObjectScript obj)
        {
            return new ObjectPool<ObjectScript>(() =>
            {
                return Instantiate(obj);
            }, obj =>
            {
                obj.gameObject.SetActive(true);
            }, obj =>
            {
                obj.gameObject.SetActive(false);
            }, obj =>
            {
                Destroy(obj.gameObject);
            });
        }
        private ObjectPool<ParticleSystem> InitializeParticlePool(ParticleSystem obj)
        {
            return new ObjectPool<ParticleSystem>(() =>
            {
                return Instantiate(obj);
            }, obj =>
            {
                obj.gameObject.SetActive(true);
            }, obj =>
            {
                obj.gameObject.SetActive(false);
            }, obj =>
            {
                Destroy(obj.gameObject);
            });
        }
        #endregion

        #region Pool Get and Release Methods
        /// <summary>
        ///  Summon a particle from pool.
        /// </summary>
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
        /// <summary>
        ///  Summon a prefab from pool.
        /// </summary>
        public void PrefabSpawn(PrefabToSpawn prefab, Vector3 spawnLocation)
        {
            switch (prefab)
            {
                case PrefabToSpawn.Capsule:
                    var capsule = _capsulePrefabPool.Get();
                    capsule.transform.position = spawnLocation;
                    _capsulePrefabQueue.Enqueue(capsule);
                    break;
                case PrefabToSpawn.Box:
                    var box = _boxPrefabPool.Get();
                    box.transform.position = spawnLocation;
                    _boxPrefabQueue.Enqueue(box);
                    break;
                case PrefabToSpawn.Sphere:
                    var sphere = _spherePrefabPool.Get();
                    sphere.transform.position = spawnLocation;
                    _spherePrefabQueue.Enqueue(sphere);
                    break;
            }
        }
        /// <summary>
        ///  Removes every object that summoned from any object pool.
        /// </summary>
        public void ReleaseAllPrefabPools()
        {
            for (int i = 0; i < _capsulePrefabQueue.Count; i++)
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
