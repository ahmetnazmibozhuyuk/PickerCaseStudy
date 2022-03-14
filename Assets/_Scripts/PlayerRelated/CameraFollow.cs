using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform _target;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z);
    }
}
