using UnityEngine;
[RequireComponent(typeof(Collider))]
public class SpawnerTrigger : MonoBehaviour
{
    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponentInParent<Spawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _spawner.ActivateSpawner();
    }
}
