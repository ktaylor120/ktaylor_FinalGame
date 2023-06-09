using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2FightCon : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 10;

    private Animator animator; // Reference to the Animator component
    private P2Movement movementScript; // Reference to the Movem script
   

    private void Start()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<P2Movement>(); // Get the Movem script component
    }

    private void Update()
    {

        // Check if spacebar is pressed for attack
        if (Input.GetKeyDown(KeyCode.P))
        {

            {
                PerformAttack();
                return;
            }
        }

    }

    private void PerformAttack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

        movementScript.SetIsAttacking(true); // Disable movement in the Movem script
    }

    private void EndAttackAnimation()
    {

        movementScript.SetIsAttacking(false); // Enable movement in the Movem script
    }


    private void HitBoxEvent()
    {
        // Detect enemies
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Damage>().TakeDamage(attackDamage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
