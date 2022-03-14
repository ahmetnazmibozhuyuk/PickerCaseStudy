using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    //private ISpawnerActivate _spawner;

    private void Awake()
    {
        //_spawner = GetComponentInParent<MonoBehaviour>().GetComponent<ISpawnerActivate>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //_spawner.ActivateBall();
        Debug.Log(other);
    }
}
