using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    public GameObject textBox;
    public Text signText;
    public bool signActive;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && signActive == true)
        {
            if(textBox.activeInHierarchy)
            {
                textBox.SetActive(false);
                animator.SetBool("Sign", false);
            } else 
            {
                textBox.SetActive(true);
                animator.SetBool("Sign", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            signActive = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            signActive = false;
            textBox.SetActive(false);
        }   
    }
}
