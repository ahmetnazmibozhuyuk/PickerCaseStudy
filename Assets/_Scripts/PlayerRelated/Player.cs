using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Controller))]
public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Controller _controller;
    private Collider _playerCollider;

    [SerializeField] private AudioSource popSound;
    //@TODO: Kamerayı ve kamera kolunu düzgün bir şekilde bağlamanın bir yolunu düşün.
    //@TODO: Toplanan objelerin collisionu continuous dynamic ve playerın collisionu continuous olursa arıza az ama performans kötü.

    //@todo: Spawn noktasını düzgün ayarla!

    private void Awake()
    {
        //GameManager.Instance.Player = this;
        _rigidbody = GetComponent<Rigidbody>();
        _controller = GetComponent<Controller>();
        _playerCollider = GetComponent<Collider>();

    }
    private void Update()
    {


    }


    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_controller.Movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDER HIT!!");
        popSound.Play();
        
    }
}
