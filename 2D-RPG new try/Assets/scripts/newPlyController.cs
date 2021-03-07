using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newPlyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rbPly;
    public Animator plyAnim;
    public healthbar playerHealthbar;
    public int playerCurrentHealth;
    public int maxHealth;
    public bool ded;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    void Start() 
    {
        playerCurrentHealth = maxHealth;
        playerHealthbar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(playerCurrentHealth <= 0) {
            rbPly.velocity = Vector2.zero;
            rbPly.angularVelocity = 0;
            ded = true;
            StartCoroutine(restart());
        }
    }

    void FixedUpdate() 
    {
        if(ded == false) 
        {
            rbPly.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

            Vector2 lookDirec = mousePos - rbPly.position;
            float angle = Mathf.Atan2(lookDirec.y, lookDirec.x) * Mathf.Rad2Deg + 90f; //Sagt um wie viel Grad sich der Player drehen soll, abhängig von der Mausposition
            rbPly.rotation = angle;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("bullet")) {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int _amount) {
        plyAnim.SetBool("Hurt", true);
        playerCurrentHealth -= _amount;
        playerHealthbar.SetHealth(playerCurrentHealth);
        StartCoroutine(backToIdle());
    }
    private IEnumerator backToIdle() {
        yield return new WaitForSeconds(0.5f);
        plyAnim.SetBool("Hurt", false);
    }

    private IEnumerator restart()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("randomLevelTest");
    }
}
