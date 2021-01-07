using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    // Update is called once per frame, used for processing inputs
    void Update()
    {
        processInput();
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        Move();
    }

    void processInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized; //new Vector2, da der Vector2 oben nur die "Blaupause" für den angewendeten Vector ist

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", movement.sqrMagnitude); //Warum movement²?

        safeLastMove();

        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Attack", true); //Attack wird ausgelöst
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Shoot", true); //Shoot ausgelöst
        }

    }

    void safeLastMove()
    {
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", 0);
        }
        else if(Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("LastHorizontal", 0);
        }
    }

    void Move()
    {
       if(animator.GetBool("Attack") == true)
       {
           rb.velocity = Vector2.zero; //Während Attack kein Laufen möglich
       }
       else if(animator.GetBool("Shoot") == true)
       {
           rb.velocity = Vector2.zero;
       }
       else if (animator.GetBool("Sign") == true)
       {
           rb.velocity = Vector2.zero;
       }
        else
       {
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
       }
    }

    void StopAttack()
    {
        if(animator.GetBool("Attack")) //Am Ende der Attack-Animation über Event getriggert -> Attack beendet
            animator.SetBool("Attack", false);
    }

    void StopShoot()
    {
        if(animator.GetBool("Shoot"))
            animator.SetBool("Shoot", false);
    }

    
}   

