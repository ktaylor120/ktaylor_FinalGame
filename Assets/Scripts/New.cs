using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Camera characterCamera; // Reference to the character's camera

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    private Animator animator; // Reference to the Animator component
    private bool isAttacking = false; // Flag to track if the character is currently attacking

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking) // If the character is currently attacking, disable movement
            return;

        // Get input from the horizontal and vertical axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera's forward direction
        Vector3 cameraForward = characterCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        Vector3 movement = (cameraForward * vertical + characterCamera.transform.right * horizontal).normalized;

        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Update the animator parameters based on movement
        UpdateAnimatorParameters(horizontal, vertical);

        // Check if spacebar is pressed for attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }
    }

    private void UpdateAnimatorParameters(float horizontal, float vertical)
    {
        // Calculate the absolute values of horizontal and vertical input
        float absHorizontal = Mathf.Abs(horizontal);
        float absVertical = Mathf.Abs(vertical);

        // Set the animator parameters based on the input values
        animator.SetFloat("Speed", Mathf.Max(absHorizontal, absVertical));
    }

    private void PerformAttack()
    {
        if (isAttacking) // If the character is already attacking, ignore the input
            return;

        // Trigger the attack animation
        animator.SetTrigger("Attack");
        isAttacking = true;

        // Detect enemies
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Damage> ().TakeDamage(attackDamage);
        }
    }

    private void EndAttackAnimation()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
