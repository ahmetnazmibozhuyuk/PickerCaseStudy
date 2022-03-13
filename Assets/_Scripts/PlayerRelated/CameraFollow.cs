using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform _target;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y, _target.position.z);
    }
}
