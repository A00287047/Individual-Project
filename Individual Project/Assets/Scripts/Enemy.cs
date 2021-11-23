using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damage
{
    public float healthPool = 10f;
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;
    private float currentHealth = 10f;

    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;


    void Start()
    {
        currentHealth = healthPool;

        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update checks for "distToPlayer" and invokes ChasePlayer/StopChasing when in range
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        print("distToPlayer");

        if(distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    //Changes the direction enemy is facing depending on where player is
    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    //Invokes Die when health = 0 + plays enemy death sound
    public virtual void ApplyDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            SoundManager.sndMan.PlayEnemySound();
        }
    }

    //Destroys Enemy
    private void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}