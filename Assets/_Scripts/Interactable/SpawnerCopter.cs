using System.Collections;
using UnityEngine;
using Picker.Managers;

namespace Picker.Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpawnerCopter : Spawner
    {
        [Tooltip("Select which prefab to spawn when copter is activated.")]
        [SerializeField] private PrefabToSpawn objectToSpawn;

        [Tooltip("The copter will follow the targets in numerical order when activated. Amount of targets can be increased or decreased by simply modifying this array.")]
        [SerializeField] private Transform[] target;

        [SerializeField] private float speed;
        [Tooltip("The copter will spawn a prefab every spawnPeriod second(s).")]
        [SerializeField] private float spawnPeriod;

        [Tooltip("Total prefab spawn amount.")]
        [SerializeField] private int spawnAmount;

        private Vector3 _moveTowardsPosition;

        private Vector3[] _targetPosition;

        private int _currentTargetIndex;

        private Rigidbody _rigidbody;
        private bool _copterIsActive = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _targetPosition = new Vector3[target.Length];
            for (int i = 0; i < _targetPosition.Length; i++)
            {
                _targetPosition[i] = target[i].position;
            }
        }
        private void FixedUpdate()
        {
            CopterMovement();
        }
        public override void ActivateSpawner()
        {
            _copterIsActive = true;
            StartCoroutine(Co_PeriodicSpawn());
        }
        private void CopterMovement()
        {
            if (!_copterIsActive) return;

            if (transform.position != _targetPosition[_currentTargetIndex])
            {
                _moveTowardsPosition = Vector3.MoveTowards(transform.position, _targetPosition[_currentTargetIndex], speed * Time.deltaTime);
                _rigidbody.MovePosition(_moveTowardsPosition);
            }
            else
            {
                if (_currentTargetIndex < _targetPosition.Length - 1)
                {
                    _currentTargetIndex++;
                }
                else
                {
                    _copterIsActive = false;
                }
            }
        }
        private IEnumerator Co_PeriodicSpawn()
        {

            yield return new WaitForSeconds(spawnPeriod);
            if (spawnAmount > 0 && _copterIsActive)
            {
                spawnAmount--;
                PoolManager.instance.PrefabSpawn(objectToSpawn, new Vector3(transform.position.x,transform.position.y-1f,transform.position.z));
                StartCoroutine(Co_PeriodicSpawn());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}


