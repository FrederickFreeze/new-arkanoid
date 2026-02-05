using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestControl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PlayerInput playerInput;
    private InputAction moveAction;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void Update()
    {
        // Считываем значение КАЖДЫЙ КАДР - это ключевой момент
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Плавное движение
        Vector2 movement = new Vector2(moveInput.x, moveInput.y) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
    /*
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        transform.Translate(new Vector2(input.x, 0));
    }*/
}
