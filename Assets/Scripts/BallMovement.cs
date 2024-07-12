using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    private PlayerInput playerInput;
    public float speed = 5f;
    private PlayerInputActions playerInputActions;
    public static BallMovement instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        //rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        rb.velocity = new Vector3(inputVector.x, 0, inputVector.y) * speed;
    }
}
