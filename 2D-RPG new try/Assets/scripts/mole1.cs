//Tutorial Part 17 weiter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole1 : enemy
{
    public GameObject Enemy1;
    public Rigidbody2D rigidEnemy;
    public Animator animator;
    public Animator playerAnimator;
    int currentHealth;
    public int playerHealth = 100;
    private bool canAttack = true;
    private bool enemyRage = false;

    //movement:
    public Transform target;
    public float detectRadius;
    public float stopRadius;
    public float attackRadius;

    /*knockback:
    public float impact;
    public float knockTime;
    private Vector2 difference; */

    void Start() 
    {
        currentHealth = maxHealth;
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        checkDistance();
        controlAttack();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("arrow"))
        {
            currentHealth -= 25;

            animator.SetBool("isHurt", true);
            enemyRage = true;
            StartCoroutine(backToIdle());

            /*Knockback
            if (rigidEnemy != null)
            {
            rigidEnemy.isKinematic = false;
            Vector2 difference = rigidEnemy.transform.position - transform.position;
            difference = difference.normalized * impact;
            rigidEnemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockStop(rigidEnemy));
            } */

            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(enemyDead());
        this.enabled = false;
    }
    private IEnumerator enemyDead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private IEnumerator backToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isHurt", false);
    }

    /* private IEnumerator knockStop(Rigidbody2D rigidEnemy)
    {
        if(rigidEnemy != null) {
        yield return new WaitForSeconds(knockTime);
        rigidEnemy.velocity = Vector2.zero;
        rigidEnemy.isKinematic = true;
        }
    } */

    public void checkDistance()
    {
        if(Vector2.Distance(target.position, transform.position) <= detectRadius && Vector2.Distance(target.position, transform.position) > stopRadius || enemyRage == true)
        {
            Vector2 moving = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rigidEnemy.MovePosition(moving);
            //X und Y Werte der Bewegung an Animator übermitteln -> Animationen werden aufgerufen
            animator.SetFloat("horizontal", - (transform.position.x - target.position.x));
            animator.SetFloat("vertical", - (transform.position.y - target.position.y));

            animator.SetBool("walk", true);
        }

        else
        {
            animator.SetBool("walk", false);
        }
    }

    //Enemy Attacks und Player-Schaden Management + Player Tod:
    public void controlAttack()
    {
        if(Vector2.Distance(target.position, transform.position) <= attackRadius && canAttack == true)
        {
            StartCoroutine(hitInterval());
        }
        else if(playerHealth <= 0)
        {
            Debug.Log("Player dead");
        }
    }

    public IEnumerator hitInterval()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);

        if(Vector2.Distance(target.position, transform.position) <= attackRadius)
        {
            playerHealth -= 20;
            playerAnimator.SetBool("isHurt", true);
            animator.SetBool("attack", true);
            enemyRage = false;
            StartCoroutine(backtoPlayerIdle());
            StartCoroutine(attackToIdle());
        }
        else {
            canAttack = true;
        }
    }

    public IEnumerator backtoPlayerIdle()
    {
        yield return new WaitForSeconds(0.4f);
        playerAnimator.SetBool("isHurt", false);
        canAttack = true;
    }
    public IEnumerator attackToIdle()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("attack", false);
    }
}