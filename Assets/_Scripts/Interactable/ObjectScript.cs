using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Picker.Interactable
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ObjectScript : MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {
            //GameManager.Instance.PlaySound();
        }
    }
}

