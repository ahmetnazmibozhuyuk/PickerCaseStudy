using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Vector3 Movement { get; private set; }


    private Vector3 direction;

    [SerializeField] private float width;
    [SerializeField] private float playerSpeed;

    private float _xDisplacement;

    private float _clickCenterX;
    private float _playerDownPositionX;


    private Touch _touch;

    private Vector3 _leftFallPoint;
    private Vector3 _rightFallPoint;

    //public Vector3 Direction;
    //@TODO REMOVE MAGIC NUMBERS!

    [SerializeField] private float sensitivity = 0.02f;
    [SerializeField] private float forwardSpeed = 0.1f;

    [SerializeField] private SelectController selectController;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _touch.phase = TouchPhase.Ended;
    }
    private void Update()
    {
        switch (selectController)
        {
            case SelectController.MouseController:
                MouseController();
                break;
            case SelectController.TouchController:
                TouchController();
                break;
        }
        SetMovement();
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(new Vector3((transform.position.x + (Movement.x - transform.position.x) * playerSpeed * Time.fixedDeltaTime), Movement.y, Movement.z));

    }
    private void InputActivated()
    {
        if (GameManager.Instance.CurrentState == GameState.GameAwaitingStart) GameManager.Instance.ChangeState(GameState.GameStarted);
        _clickCenterX = Input.mousePosition.x;
        _playerDownPositionX = transform.position.x;
    }
    private void InputIsActive() //@TODO DÜZGÜN ŞEKİLDE REFACTOR ETMEYE ÇALIŞ!
    {
        _xDisplacement = (Input.mousePosition.x - _clickCenterX) * sensitivity;

    }
    private void InputDeactivated()
    {
        _xDisplacement = 0;
        _playerDownPositionX = transform.position.x;
    }

    private void SetMovement()
    {
        if (GameManager.Instance.CurrentState == GameState.GameStarted)
        {
            float xPos;

            if (_playerDownPositionX + _xDisplacement > width)
            {

                xPos = width;
                _clickCenterX = Input.mousePosition.x;
                _xDisplacement = 0;
                _playerDownPositionX = transform.position.x;
            }
            else if (_playerDownPositionX + _xDisplacement < -width)
            {
                xPos = -width;
                _clickCenterX = Input.mousePosition.x;
                _xDisplacement = 0;
                _playerDownPositionX = transform.position.x;
            }
            else
            {
                xPos = _playerDownPositionX + _xDisplacement;
            }
            //xPos = _playerDownPositionX + _xDisplacement;
            Movement = new Vector3(xPos, transform.position.y, transform.position.z + forwardSpeed);
            //Movement = new Vector3(Mathf.Clamp(_playerDownPositionX + _xDisplacement, -width, width), transform.position.y, transform.position.z + forwardSpeed);
            //if(_playerDownPositionX + _xDisplacement > width || _playerDownPositionX + _xDisplacement < -width)
            //{
            //    _clickCenterX = Input.mousePosition.x;
            //    _xDisplacement = 0;
            //    _playerDownPositionX = transform.position.x;
            //}
        }
        else
        {
            Movement = new Vector3(Mathf.Clamp(_playerDownPositionX + _xDisplacement, -width, width), transform.position.y, transform.position.z);
        }
        direction = (Movement - transform.position).normalized;

    }
    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputActivated();
        }
        if (Input.GetMouseButton(0))
        {
            InputIsActive();
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputDeactivated();
        }
    }
    private void TouchController()
    {

        if (Input.touchCount > 0) _touch = Input.GetTouch(0);
        else return;

        if (_touch.phase == TouchPhase.Began)
        {
            InputActivated();
        }
        if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary)
        {
            InputIsActive();
        }
        if (_touch.phase == TouchPhase.Ended)
        {
            InputDeactivated();
        }
    }
}

enum SelectController
{
    MouseController = 0, TouchController = 1
}