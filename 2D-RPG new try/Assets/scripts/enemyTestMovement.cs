using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTestMovement : MonoBehaviour
{
    public float attackRadius;
    public Rigidbody2D rb;
    public Transform target;
    public healthbar enemyHealthbar;
    private int enemyCurrentHealth;
    public int maxHealth = 100;
    private bool ded = false;
    public Animator enemyAnim;
    private bool canShoot = true;
    private bool playerFound;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public GameObject coin;

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
            // soundManager.sManagerInstance.Audio.PlayOneShot(soundManager.sManagerInstance.enemyDeath);
            StartCoroutine(death());
        }
    }

    private void checkDistance()
    {
        if (Vector2.Distance(target.position, transform.position) < attackRadius)
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
            enemyHealthbar.SetHealth(enemyCurrentHealth);
            enemyAnim.SetBool("Hurt", true);
            StartCoroutine(backToIdle());
        }
    }
    private IEnumerator backToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("Hurt", false);
    }

    private IEnumerator controlShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.5f);
        if (ded == false) {
            soundManager.sManagerInstance.Audio.PlayOneShot(soundManager.sManagerInstance.smallTurret);
            enemyAnim.SetBool("shot", true);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
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
        Instantiate(coin, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
