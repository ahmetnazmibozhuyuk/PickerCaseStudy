using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 Movement { get; private set; }

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


        //Debug.Log("can move left = " + CanMoveLeft() + " can move right = " + CanMoveRight());
    }
    private void FixedUpdate()
    {

    }
    private bool CanMoveLeft()
    {
        _leftFallPoint = new Vector3(transform.localPosition.x - 2.1f, transform.position.y, transform.position.z);
        return Physics.Raycast(_leftFallPoint, -Vector3.up, 5);
    }
    private bool CanMoveRight()
    {
        _rightFallPoint = new Vector3(transform.localPosition.x + 2.1f, transform.position.y, transform.position.z);
        return Physics.Raycast(_rightFallPoint, -Vector3.up, 5);
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

        if (_xDisplacement < 0 && !CanMoveLeft())
        {
            _clickCenterX = Input.mousePosition.x;
            _xDisplacement = 0;
            _playerDownPositionX = transform.position.x;
        }
        if (_xDisplacement > 0 && !CanMoveRight())
        {
            _clickCenterX = Input.mousePosition.x;
            _xDisplacement = 0;
            _playerDownPositionX = transform.position.x;
        }

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
            Movement = new Vector3(Mathf.Clamp(_playerDownPositionX + _xDisplacement, -3f, 3f), transform.position.y, transform.position.z + forwardSpeed);
        }
        else
        {
            Movement = new Vector3(Mathf.Clamp(_playerDownPositionX + _xDisplacement, -3f, 3f), transform.position.y, transform.position.z);
        }

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
        //switch (_touch.phase)
        //{

        //}
        if (Input.touchCount > 0) _touch = Input.GetTouch(0);

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
    //void OnDrawGizmosSelected()
    //{

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, _leftFallPoint);
    //    Gizmos.DrawLine(transform.position, _rightFallPoint);

    //}
}

enum SelectController
{
    MouseController = 0, TouchController = 1
}