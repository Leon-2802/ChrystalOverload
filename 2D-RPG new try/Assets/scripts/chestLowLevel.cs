using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestLowLevel : MonoBehaviour
{
   public Animator chestAnim;
   public levelGeneration levelScr;
   public controlChests chestScr;
   private bool playerInRange;

    private void Start() {
        levelScr = GameObject.FindWithTag("levelGeneration").GetComponent<levelGeneration>();
        chestScr = GameObject.FindWithTag("controlChests").GetComponent<controlChests>();
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange == true && chestScr.keyFound == true)  {
            chestAnim.SetTrigger("open");
        }
        else if(levelScr.rowCounter < 2) {
            Destroy(gameObject);
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
