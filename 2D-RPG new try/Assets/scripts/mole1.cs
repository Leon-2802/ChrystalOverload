//Tutorial Part 17 weiter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mole1 : enemy
{
    public GameObject Enemy1;
    public Rigidbody2D rigidEnemy;
    public Rigidbody2D rigidPlayer;
    public Animator animator;
    public Animator playerAnimator;
    int currentHealth;
    public healthbar moleHealthbar;
    public int playerCurrentHealth;
    public healthbar playerHealthbar;
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

    private void Start() 
    {
        currentHealth = maxHealth;
        moleHealthbar.SetMaxHealth(maxHealth);
        playerCurrentHealth = maxHealth;
        playerHealthbar.SetMaxHealth(maxHealth);

        target = GameObject.FindWithTag("Player").transform;
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

            if(currentHealth <= 0)
            {
                Die();
            }
            else 
            {
                animator.SetBool("isHurt", true);
                enemyRage = true;
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
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        rigidEnemy.velocity = Vector2.zero;
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
    }

    /* private IEnumerator knockStop(Rigidbody2D rigidEnemy)
    {
        if(rigidEnemy != null) {
        yield return new WaitForSeconds(knockTime);
        rigidEnemy.velocity = Vector2.zero;
        rigidEnemy.isKinematic = true;
        }
    } */

    private void checkDistance()
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
    private void controlAttack()
    {
        if(Vector2.Distance(target.position, transform.position) <= attackRadius && canAttack == true)
        {
            animator.SetBool("inRange", true);
            StartCoroutine(hitInterval());
        }
        else if(playerCurrentHealth <= 0)
        {
            rigidPlayer.velocity = Vector2.zero;
            playerAnimator.SetBool("isDead", true);
            StartCoroutine(destroyPlayer());
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
            enemyRage = false;
            StartCoroutine(attackToIdle());

            if(playerAnimator.GetBool("Block") == false)
            {
                playerCurrentHealth -= 20;
                playerHealthbar.SetHealth(playerCurrentHealth);
                //Animation:
                playerAnimator.SetBool("isHurt", true);
                StartCoroutine(backtoPlayerIdle());
            }

        }
        else {
            canAttack = true;
            animator.SetBool("inRange", false);
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
        if(playerAnimator.GetBool("Block") == true)
        {
            canAttack = true;
        }
    }
    private IEnumerator destroyPlayer()
    {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel("Stage 1");
    }
}