using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    public GameObject hitParticlePrefab;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        // Instantiate hit particle effect at the object's position
        Vector3 spawnPosition = transform.position + Vector3.up;
        Instantiate(hitParticlePrefab, spawnPosition, Quaternion.identity);


        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Dead");
        //Die animation
        animator.SetBool("IsDead", true);

        //Disable the enemy
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}