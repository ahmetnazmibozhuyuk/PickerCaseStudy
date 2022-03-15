using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform target;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
    }
}
