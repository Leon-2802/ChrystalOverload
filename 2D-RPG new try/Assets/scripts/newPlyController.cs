using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rbPly;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate() 
    {
        rbPly.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        Vector2 lookDirec = mousePos - rbPly.position;
        float angle = Mathf.Atan2(lookDirec.y, lookDirec.x) * Mathf.Rad2Deg + 90f; //Sagt um wie viel Grad sich der Player drehen soll, abhängig von der Mausposition
        rbPly.rotation = angle;
    }
}
