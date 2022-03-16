using System;
using UnityEngine;
using Picker.Managers;

namespace Picker.PlayerControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class Controller : MonoBehaviour
    {
        public Vector3 Movement { get; private set; }

        [SerializeField] [Range(0.001f, 0.05f)] private float sensitivity = 0.02f;
        [SerializeField] private float forwardSpeed = 0.1f;
        [SerializeField] private float width;

        [SerializeField] private SelectController selectController;

        private float _xDisplacement;
        private float _clickCenterX;
        private float _playerDownPositionX;
        private float _localX;

        private Touch _touch;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _touch.phase = TouchPhase.Ended;
        }
        private void Update()
        {
            ChooseController();
            ClampMovement();
            SetMovement();
        }
        private void FixedUpdate()
        {
            _rigidbody.MovePosition(Movement);
        }
        #region Input Action
        private void InputActivated()
        {
            if (GameManager.Instance.CurrentState == GameState.GameAwaitingStart || GameManager.Instance.CurrentState == GameState.GameWon)
                GameManager.Instance.ChangeState(GameState.GameStarted);
            _clickCenterX = Input.mousePosition.x;
            _playerDownPositionX = transform.position.x;
        }
        private void InputIsActive()
        {
            _xDisplacement = (Input.mousePosition.x - _clickCenterX) * sensitivity;
        }
        private void InputDeactivated()
        {
            _xDisplacement = 0;
            _playerDownPositionX = transform.position.x;
        }
        // I used this method to clamp player position instead of using a simple Mathf.Clamp because this method also resets the 
        // dragging position to prevent 
        private void ClampMovement()
        {
            if (_playerDownPositionX + _xDisplacement >= width)
            {
                _xDisplacement = 0;
                _playerDownPositionX = transform.position.x;
                _clickCenterX = Input.mousePosition.x;
                _localX = width;
            }
            else if (_playerDownPositionX + _xDisplacement <= -width)
            {
                _xDisplacement = 0;
                _playerDownPositionX = transform.position.x;
                _clickCenterX = Input.mousePosition.x;
                _localX = -width;
            }
            else
            {
                _localX = _playerDownPositionX + _xDisplacement;
            }
        }
        private void SetMovement()
        {
            if (GameManager.Instance.CurrentState == GameState.GameStarted)
            {
                Movement = new Vector3(_localX, transform.position.y, transform.position.z + forwardSpeed);
            }
            else
            {
                Movement = new Vector3(_localX, transform.position.y, transform.position.z);
            }
        }
        #endregion

        #region Controller Spesific
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
        private void ChooseController()
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
        }
        #endregion
    }
    public enum SelectController
    {
        MouseController = 0,
        TouchController = 1,
        KeyboardController = 2
    }
}
