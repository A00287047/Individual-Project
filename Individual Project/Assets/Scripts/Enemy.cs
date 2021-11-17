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

    // Update is called once per frame
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

    public virtual void ApplyDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}