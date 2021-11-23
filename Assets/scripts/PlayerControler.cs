using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private int lineToMove = 1;
    public float lineDistance = 4;
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }
        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
            if (transform.position.y < jumpHeight)
                Jump();


        Vector3 TargetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            TargetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            TargetPosition += Vector3.right * lineDistance;


        transform.position = TargetPosition;
    }

    private void Jump()
    {
        if (controller.isGrounded) {
            dir.y = jumpForce;
        }
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }
}
