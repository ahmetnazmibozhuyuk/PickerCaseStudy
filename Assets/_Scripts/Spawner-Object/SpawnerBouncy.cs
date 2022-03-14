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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateBall()
    {
        Debug.Log("BALL IS ACTIVATED");
        _rigidbody.isKinematic = false;
    }
}
