using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerActivate
{
    public void ActivateBall();
}

public class SpawnerBouncy : MonoBehaviour, ISpawnerActivate
{


    [SerializeField] private GameObject objectToSpawn;

    private Rigidbody _rigidbody;

    void Start()
    {

    }
    private void Awake()
    {
        
    }

    void Update()
    {
        
    }
    public void ActivateBall()
    {
        Debug.Log("BALL IS ACTIVATED");
        _rigidbody.isKinematic = false;
    }
}
