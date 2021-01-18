using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionManager : MonoBehaviour
{
        public Rigidbody2D rb;
        public bool isMoving = true;
    private void OnCollisionEnter2D(Collision2D collide) 
    {
            if(collide.collider.CompareTag("Player"))
            {
                isMoving = false;
                rb.mass = 100000;
            }
    }

    private void OnCollisionExit2D(Collision2D collide) 
    {
        if(collide.collider.CompareTag("Player"))
        {
            isMoving = true;
            rb.mass = 1;
        }
    }

    private void Move() 
    {
        if(isMoving == false)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
