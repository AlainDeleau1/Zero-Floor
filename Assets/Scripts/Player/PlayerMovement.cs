using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    [SerializeField] private CharacterController player;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float fallVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private Camera mainCamera;

    private float horizontalMove;
    private float verticalMove;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 playerInput;

    private void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    private void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    private void Jump()
    {
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }
    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        SetGravity();

        Jump();

        player.Move(movePlayer * Time.deltaTime);
    }
}
