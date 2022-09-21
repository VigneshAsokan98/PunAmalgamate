using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    public PCInputActions playerInputs;
    InputAction move;

    public float speed = 10 ;
    Vector3 direction = Vector3.zero;

    PlayerController playerController;
    CharacterController characterController;
    float TurnVelocity;
    float TurnSmoothTime = 0.5f;

    public Transform cam;

    Rigidbody rb;
    private void Awake()
    {
        playerInputs = new PCInputActions();
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        move = playerInputs.Player.Move;
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
        if (playerController.photonView.IsMine)
        {
            direction = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y).normalized;
        }
        else
            cam.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (playerController.photonView.IsMine )
        {
            float targetangle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref TurnVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            transform.position += transform.forward * direction.z * speed * Time.fixedDeltaTime;
        }
    }
}
