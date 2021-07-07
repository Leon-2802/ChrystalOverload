using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cd : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public messageCtrl MessageCtrl;
    public float floatSpeed;
    [SerializeField] private string objectType = null;
    private bool inRange = false;
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
        MessageCtrl = GameObject.FindWithTag("popUp").GetComponent<messageCtrl>();
    }
    void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.5) {
            soundManager.sManagerInstance.Audio.PlayOneShot(soundManager.sManagerInstance.collectItem);
            MessageCtrl.showMessage(objectType);
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
