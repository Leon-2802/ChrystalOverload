using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestLowLevel : MonoBehaviour
{
   public Animator chestAnim;
   private bool playerInRange;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange == true) {
            chestAnim.SetTrigger("open");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }
}
