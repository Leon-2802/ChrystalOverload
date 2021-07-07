﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyLowLvl : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public controlChests storeBool;
    public messageCtrl MessageCtrl;
    public float floatSpeed;
    private bool inRange = false;
    void Start() 
    {
        target = GameObject.FindWithTag("Player").transform;
        storeBool = GameObject.FindWithTag("controlChests").GetComponent<controlChests>();
        MessageCtrl = GameObject.FindWithTag("popUp").GetComponent<messageCtrl>();
    }
    void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.5) {
            storeBool.keyFound = true;
            soundManager.sManagerInstance.Audio.PlayOneShot(soundManager.sManagerInstance.collectItem);
            MessageCtrl.showMessage("key");
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
