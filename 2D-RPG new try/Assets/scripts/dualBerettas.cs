using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dualBerettas : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public messageCtrl MessageCtrl;
    public float floatSpeed;
    private bool inRange = false;
    private bool delay = false;
    private float delayTime = 1f;
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
        MessageCtrl = GameObject.FindWithTag("popUp").GetComponent<messageCtrl>();
    }

    void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.5) {
            soundManager.sManagerInstance.Audio.PlayOneShot(soundManager.sManagerInstance.collectItem);
            MessageCtrl.showMessage("dual");
            Destroy(gameObject);
        }
        else if (delay == true) {
            delayTime -= Time.deltaTime;
            if(delayTime <= 0) {
                inRange = true;
                delay = false;
            }
        }
        else if (inRange == true) {
            move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") {
            // StartCoroutine(destroy());
            delay = true;
        }
    }

    private void move()
    {
        Vector2 moving = Vector2.MoveTowards(transform.position, target.position, floatSpeed * Time.deltaTime);
        rb.MovePosition(moving);
    }
}
