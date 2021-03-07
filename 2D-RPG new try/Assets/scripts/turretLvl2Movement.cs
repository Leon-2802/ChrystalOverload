using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretLvl2Movement : MonoBehaviour
{
    public float attackRadius;
    public Rigidbody2D rb;
    public Transform target;
    public healthbar enemyHealthbar;
    private int enemyCurrentHealth;
    public int maxHealth = 150;
    private bool ded = false;
    public Animator enemyAnim;
    private bool canShoot = true;
    private bool playerFound;
    public GameObject bulletPrefab;
    public Transform firePointLeft;
    public Transform firePointRight;
    public float bulletSpeed;
    public GameObject coin;
    public Transform[] spawnCoins;
    public GameObject key;
    public Transform spawnKey;

    // Update is called once per frame
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;

        enemyCurrentHealth = maxHealth;
        enemyHealthbar.SetMaxHealth(maxHealth);
    }
    void FixedUpdate()
    {
        checkDistance();
        if (enemyCurrentHealth <= 0) {
            rb.rotation = 0;
            Destroy(GetComponent<PolygonCollider2D>());
            enemyAnim.SetTrigger("ded");
            ded = true;
            StartCoroutine(death());
        }
    }

    private void checkDistance()
    {
        if (Vector2.Distance(target.position, transform.position) < attackRadius || playerFound == true)
        {
            Vector2 lookdir = target.position - transform.position;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;

            if (canShoot == true && ded == false) {
                StartCoroutine(controlShoot());
            }
        }
        else if (Vector2.Distance(target.position, transform.position) > attackRadius) {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("arrow"))
        {
            rb.velocity = Vector2.zero;
            enemyCurrentHealth -= 15;
            playerFound = true;
            enemyHealthbar.SetHealth(enemyCurrentHealth);
            enemyAnim.SetBool("hurt", true);
            StartCoroutine(backToIdle());
        }
    }
    private IEnumerator backToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("hurt", false);
    }

    private IEnumerator controlShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.5f);
        if (ded == false) {
            enemyAnim.SetBool("shot", true);
            GameObject bullet1 = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
            Rigidbody2D bullet1Rb = bullet1.GetComponent<Rigidbody2D>();
            bullet1Rb.AddForce(firePointLeft.up * bulletSpeed, ForceMode2D.Impulse);
            GameObject bullet2 = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
            Rigidbody2D bullet2Rb = bullet2.GetComponent<Rigidbody2D>();
            bullet2Rb.AddForce(firePointRight.up * bulletSpeed, ForceMode2D.Impulse);
            StartCoroutine(back2Idle());
        }
        canShoot = true;
    }
    private IEnumerator back2Idle()
    {
        yield return new WaitForSeconds(0.2f);
        enemyAnim.SetBool("shot", false);
    }

    private IEnumerator death()
    {
        yield return new WaitForSeconds(0.8f);
        Instantiate(coin, spawnCoins[0].position, transform.rotation);
        Instantiate(coin, spawnCoins[1].position, transform.rotation);
        Instantiate(coin, spawnCoins[2].position, transform.rotation);
        Instantiate(key, spawnKey.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
