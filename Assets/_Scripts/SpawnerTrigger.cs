using UnityEngine;
[RequireComponent(typeof(Collider))]
public class SpawnerTrigger : MonoBehaviour
{
    private ISpawnerActivate _spawner;

    private void Awake()
    {
        _spawner = GetComponentInParent<ISpawnerActivate>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _spawner.ActivateSpawner();
    }
}
