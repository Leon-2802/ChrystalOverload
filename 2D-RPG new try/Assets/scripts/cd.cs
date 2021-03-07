using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cd : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public float floatSpeed;
    private bool inRange = false;
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.5) {
            Destroy(gameObject);
        }
        else if (inRange == true) {
            move();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") {
            // StartCoroutine(destroy());
            inRange = true;
        }
    }
    private void move()
    {
        Vector2 moving = Vector2.MoveTowards(transform.position, target.position, floatSpeed * Time.deltaTime);
        rb.MovePosition(moving);
    }
}
