using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movem : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Camera characterCamera; // Reference to the character's camera
    private Animator animator; // Reference to the Animator component
    private bool isAttacking = false; // Flag to track if the character is currently attacking

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking) // If the character is currently attacking, disable movement
        {
            // Update the animator parameters based on movement (set speed to 0)
            UpdateAnimatorParameters(0f, 0f);
            return;
        }

        // Get input from the horizontal and vertical axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera's forward direction
        Vector3 cameraForward = characterCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        Vector3 movement = (cameraForward * vertical + characterCamera.transform.right * horizontal).normalized;

        // Update the animator parameters based on movement
        UpdateAnimatorParameters(horizontal, vertical);

        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void UpdateAnimatorParameters(float horizontal, float vertical)
    {
        // Calculate the absolute values of horizontal and vertical input
        float absHorizontal = Mathf.Abs(horizontal);
        float absVertical = Mathf.Abs(vertical);

        // Set the animator parameters based on the input values
        animator.SetFloat("Speed", Mathf.Max(absHorizontal, absVertical));
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
}
