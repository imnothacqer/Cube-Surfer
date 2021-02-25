using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    public bool canSlide;

    public float movementSpeed;
    public float slideSensitivity;

    public Vector3 moveDirection;
    public Vector3 slideDirection;

    private float inputHorizontal;

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (canSlide)
        {
            inputHorizontal = SimpleInput.GetAxis("Horizontal");
            Vector3 mov = slideDirection * (inputHorizontal * slideSensitivity);
            transform.Translate(mov * Time.deltaTime);
        }
    }
}
