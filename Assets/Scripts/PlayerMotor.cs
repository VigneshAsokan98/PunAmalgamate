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
    public float TurnSmoothTime = 0.1f;

    public Transform cam;

    Rigidbody rb;

    public Animator animator;
    Vector3 gravity;
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


        if (playerController.photonView.IsMine)
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref TurnVelocity, TurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;

                characterController.Move(moveDir.normalized * speed * Time.deltaTime);

                //transform.position += transform.forward * direction.z * speed * Time.fixedDeltaTime;
            
                animator.SetBool("IsRunning", true);
            }
            else
                animator.SetBool("IsRunning", false);
        }

        gravity.y += -9.81f * Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);
    }
}
