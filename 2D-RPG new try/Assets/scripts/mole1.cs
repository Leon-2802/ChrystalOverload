//Tutorial Part 17 weiter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class mole1 : enemy
{
    public GameObject Enemy1;
    public Rigidbody2D rigidEnemy;
    NavMeshAgent agent;
    public Rigidbody2D rigidPlayer;
    public Animator animator;
    public Animator playerAnimator;
    int currentHealth;
    public healthbar moleHealthbar;
    public PlayerController playerScript;
    public int playerCurrentHealth;
    public healthbar playerHealthbar;
    public soundManagerScript callSounds;
    private bool canAttack = true;
    // private bool enemyRage = false;

    //movement:
    public Transform target;
    public float detectRadius;
    public float stopRadius;
    public float attackRadius;
    public bool dontWalk = false;
    public bool enemyRage = false;

    /*knockback:
    public float impact;
    public float knockTime;
    private Vector2 difference; */

    private void Start() 
    {
        currentHealth = maxHealth;
        moleHealthbar.SetMaxHealth(maxHealth);
        playerCurrentHealth = maxHealth;
        playerHealthbar.SetMaxHealth(maxHealth);

        target = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void FixedUpdate()
    {
        if(currentHealth <= 0)
            return;

        checkDistance();
        controlAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("arrow"))
        {
            currentHealth -= 25;
            moleHealthbar.SetHealth(currentHealth);
            moveSpeed = 2;

            if(currentHealth <= 0)
            {
                Die();
            }
            else 
            {
                animator.SetBool("isHurt", true);
                // enemyRage = true;
                StartCoroutine(backToIdle());
            }
            

            /*Knockback
            if (rigidEnemy != null)
            {
            rigidEnemy.isKinematic = false;
            Vector2 difference = rigidEnemy.transform.position - transform.position;
            difference = difference.normalized * impact;
            rigidEnemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockStop(rigidEnemy));
            } */    
        }
        else if (collision.collider.CompareTag("Player"))
        {
            rigidEnemy.velocity = Vector2.zero;
            rigidEnemy.angularVelocity = 0;
            dontWalk = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            dontWalk = false;
        }
    }

    private void Die()
    {
        animator.SetTrigger("ded");
        dontWalk = true;
        Enemy1.GetComponent<CapsuleCollider2D>().enabled = false;
        moleHealthbar.gameObject.SetActive(false);
        rigidEnemy.velocity = Vector2.zero;
        rigidEnemy.angularVelocity = 0;
        // callSounds.Play("enemyDeath");
        // playerScript.enemyDeaths++;
        StartCoroutine(enemyDead());
        //this.enabled = false;
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
        enemyRage = true;
    }

    /* private IEnumerator knockStop(Rigidbody2D rigidEnemy)
    {
        if(rigidEnemy != null) {
        yield return new WaitForSeconds(knockTime);
        rigidEnemy.velocity = Vector2.zero;
        rigidEnemy.isKinematic = true;
        }
    } */

    //Checkt ob Player zu nah/zu weit ist im FixedUpdate, wenn zu nah/zu weit -> Enemy bleibt stehen:
    private void checkDistance()
    {
        if (enemyRage == false) 
        {
            if (Vector2.Distance(target.position, transform.position) <= stopRadius || Vector2.Distance(target.position, transform.position) >= detectRadius || animator.GetBool("isHurt") == true) 
            {
                dontWalk = true;
                animator.SetBool("walk", false);
                rigidEnemy.velocity = Vector2.zero;
                rigidEnemy.angularVelocity = 0;
            }
            else
            {
                dontWalk = false;
            }
        }
        else 
        {
            if(Vector2.Distance(target.position, transform.position) <= stopRadius ||animator.GetBool("isHurt") == true)
            {
                dontWalk = true;
                animator.SetBool("walk", false);
                rigidEnemy.velocity = Vector2.zero;
                rigidEnemy.angularVelocity = 0;
            }
            else
            {
                dontWalk = false;
            }
        }
    }

    //Enemy Attacks und Player-Schaden Management + Player Tod:
    private void controlAttack()
    {
        if(Vector2.Distance(target.position, transform.position) <= attackRadius && canAttack == true)
        {
            animator.SetBool("inRange", true);
            StartCoroutine(hitInterval());
        }
        else if(playerCurrentHealth <= 0)
        {
            playerAnimator.SetBool("isDead", true);
            StartCoroutine(destroyPlayer());
        }
        else if (Vector2.Distance(target.position, transform.position) > attackRadius) {
            animator.SetBool("inRange", false);
        }
    }

    //Alle IEnumerators, welche mit Attack des Gegners verbunden sind:
    private IEnumerator hitInterval()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);

        if(Vector2.Distance(target.position, transform.position) <= attackRadius)
        { 
            animator.SetBool("attack", true);
            // enemyRage = false;
            StartCoroutine(attackToIdle());

            if(playerAnimator.GetBool("Block") == false)
            {
                    playerCurrentHealth -= 20;
                    playerHealthbar.SetHealth(playerCurrentHealth);
                    //Animation:
                    playerAnimator.SetBool("isHurt", true);
                    StartCoroutine(backtoPlayerIdle());    
            }
            else {
                canAttack = true;
                animator.SetBool("inRange", false);
            }
        }
    }
    
    private IEnumerator backtoPlayerIdle()
    {
        yield return new WaitForSeconds(0.4f);
        playerAnimator.SetBool("isHurt", false);
        canAttack = true;
    }
    private IEnumerator attackToIdle()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("attack", false);
        animator.SetBool("inRange", false);
        canAttack = true;
    }
    private IEnumerator destroyPlayer()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene ("Stage 1");
    }
}