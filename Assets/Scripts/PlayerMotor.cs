using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    public PCInputActions playerControls;
    public InputAction move;

    public float speed = 10 ;
    Vector2 MoveDirection = Vector2.zero;
    private void Awake()
    {
        playerControls = new PCInputActions();
    }
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
    void Start()
    {
    }

    void Update()
    {
        MoveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(MoveDirection.x * Time.fixedDeltaTime * speed, 0, MoveDirection.y * Time.fixedDeltaTime * speed);
    }
}
