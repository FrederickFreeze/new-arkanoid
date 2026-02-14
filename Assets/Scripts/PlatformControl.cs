using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class Control : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 10f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.1f;
    [SerializeField] private Camera _camera;
    private GameInput _gameInput;
    private bool _pressContinued = false;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gameInput = new GameInput();
        _gameInput.PlatformMovement.Press.started += ctx => _pressContinued = true;
        _gameInput.PlatformMovement.Press.canceled += ctx => { 
            _pressContinued = false;
            if (GameManager.instance.IsCurrentState(GameState.AwaitingStart))
            {
                GameManager.instance.SetState(GameState.Playing);
            }
        };
        _gameInput.Enable();
    }

    private void OnEnable() => _gameInput.Enable();
    private void OnDisable() => _gameInput.Disable();

    private void FixedUpdate()
    {
        if (_pressContinued)
        {
            Vector2 currentCoord = _camera.ScreenToWorldPoint(_gameInput.PlatformMovement.GetCoordinates.ReadValue<Vector2>());
            Vector2 direction = currentCoord - _rb.position;
            if(Mathf.Abs(direction.x) > _stoppingDistance)
            {
                direction.Normalize();
                Vector2 force = direction * _forceMultiplier;
                _rb.AddForce(force, ForceMode2D.Force);
                _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
