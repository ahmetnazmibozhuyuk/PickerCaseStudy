using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpawnerCopter : Spawner
{
    [SerializeField] private Transform[] target;
    [SerializeField] private float speed;

    [SerializeField] private float spawnPeriod;

    [SerializeField] private int spawnAmount;

    private Vector3 _moveTowardsPosition;

    private Vector3[] _targetPosition;

    [SerializeField]private int _currentTargetIndex;
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

    private void Update()
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
            _currentTargetIndex++;
            Debug.Log("NEW TARGET");
        }

    }
    private IEnumerator Co_PeriodicSpawn()
    {

        yield return new WaitForSeconds(spawnPeriod);
        if (spawnAmount > 0)
        {
            spawnAmount--;
            //Debug.Log("object spawn");
            StartCoroutine(Co_PeriodicSpawn());
        }
        else
        {
            Debug.Log("spawn bitti");
        }
    }

}
