using UnityEngine;

namespace Picker.PlayerControl
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
        }
    }
}

