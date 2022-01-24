using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public Variables
    public float speed = 3f;
    public float jumpHeight = 8f;
    public float sensitivity;
    public Transform playerCam;
    //Private Variables
    float x, z;
    bool jumping;
    Vector3 moveDirection = Vector3.zero;
    bool grounded;
    CharacterController controller;
    float xRotation;

    private void Start()
    {
        controller = GetComponent<CharacterController>();       //Get other component on GameObject
        Cursor.lockState = CursorLockMode.Locked;       //Lock Cursor
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Movement();
        Look();
    }

    void MyInput()
    {
        x = Input.GetAxis("Horizontal");    //Get player input
        z = Input.GetAxis("Vertical");
        jumping = Input.GetKey(KeyCode.Space);
    }

    void Movement()
    {
        if (grounded)   //If player is grounded
        {
            moveDirection = new Vector3(x * speed, -.75f, z * speed);   //Set movement vector based on input
            moveDirection = transform.TransformDirection(moveDirection);    //convert to world space

            if (jumping)    //If jump button pressed
            {
                moveDirection.y = jumpHeight;       //Make the player jump
            }
        }

        moveDirection.y -= 20 * Time.deltaTime;     //Gravity
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;   //Check if grounded
    }

    private float desiredX;
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, playerCam.eulerAngles.z);
        transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
}
