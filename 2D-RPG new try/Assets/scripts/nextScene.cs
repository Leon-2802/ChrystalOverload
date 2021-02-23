using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController takeScript;

   private void OnTriggerEnter2D(Collider2D other)
   {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(nextSceneLoad());
        }
   }

   private IEnumerator nextSceneLoad()
   {
        if (takeScript.allowTransition == true)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene ("randomLevelTest");
        }
        else {
            yield break;
        }
   }
}
