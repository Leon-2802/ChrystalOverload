using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTestMovement : MonoBehaviour
{
    public float attackRadius;
    public float stopRadius;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Transform target;
    public healthbar playerHealthbar;
    public int playerCurrentHealth;
    public int maxHealth = 100;

    // Update is called once per frame
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;

        playerCurrentHealth = maxHealth;
        playerHealthbar.SetMaxHealth(maxHealth);
    }
    void FixedUpdate()
    {
        checkDistance();
    }

    private void checkDistance()
    {
        if (Vector2.Distance(target.position, transform.position) > stopRadius && Vector2.Distance(target.position, transform.position) < attackRadius)
        {
            Vector2 moving = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(moving);

            Vector2 lookdir = target.position - transform.position;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;
        }
        else if (Vector2.Distance(target.position, transform.position) <= stopRadius) {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        else {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("arrow"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }


}
