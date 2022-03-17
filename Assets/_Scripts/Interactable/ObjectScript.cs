using UnityEngine;
//using Picker.Managers;

namespace Picker.Interactable
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ObjectScript : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        //private void OnCollisionEnter(Collision collision)
        //{
        //    GameManager.instance.PlaySound();
        //}
        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero; // When released back into the pool, objects may retain their speed. This line prevents 
                                                // pooled objects flying unexpectedly when re-used.
        }
    }
}

