using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowBoiController : MonoBehaviour
{
    public float attackRadius;
    public float hitRadius;
    public float stopRadius;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Transform target;
    public newPlyController plyScript;
    private int enemyCurrentHealth;
    public int maxHealth = 100;
    private bool ded = false;
    public Animator enemyAnim;
    private bool canAttack = true;
    public GameObject coin;

    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
        plyScript = GameObject.FindWithTag("Player").GetComponent<newPlyController>();

        enemyCurrentHealth = maxHealth;
    }
    void FixedUpdate()
    {
        if(ded == false) {
            checkDistance();
        }
        if (enemyCurrentHealth <= 0) {
            enemyAnim.SetBool("alive", false);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            Destroy(GetComponent<BoxCollider2D>());
            enemyAnim.SetTrigger("ded");
            rb.rotation = 0;
            ded = true;
            StartCoroutine(death());
        }
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

            if (canAttack == true && ded == false) {
                StartCoroutine(controlAttack());
            }
        }
        else if (Vector2.Distance(target.position, transform.position) > attackRadius) {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("arrow") || other.collider.CompareTag("bullet"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            enemyCurrentHealth -= 15;
            Debug.Log("enemy-health: " + enemyCurrentHealth + "/100");
            enemyAnim.SetBool("hit", true);
            StartCoroutine(backToIdle());
        }
    }
    private IEnumerator backToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("hit", false);
    }

    private IEnumerator controlAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(4f);
        enemyAnim.SetBool("readyAttack", true);
        yield return new WaitForSeconds(1f);
        if (ded == false && Vector2.Distance(target.position, transform.position) <= hitRadius) {
            enemyAnim.SetTrigger("attack");
            plyScript.TakeDamage(20);
            StartCoroutine(plyBack2Idle());
        }
        else {
            enemyAnim.SetTrigger("attack");
            StartCoroutine(plyBack2Idle());
        }
        canAttack = true;
    }
    private IEnumerator plyBack2Idle()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("readyAttack", false);
    }

    private IEnumerator death()
    {
        yield return new WaitForSeconds(0.8f);
        Instantiate(coin, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}

