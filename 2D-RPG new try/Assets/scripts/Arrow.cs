using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Rigidbody2D rigid;
    public Animator laserAnim; 
    private Vector2 movement;

    void OnCollisionEnter2D(Collision2D collide)
    {
        if(collide.collider.CompareTag("Player")) 
        {
            return;
        }
        else if(collide.collider.CompareTag("enemy")) 
        {
            rigid = GetComponent<Rigidbody2D>(); 
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = 0;
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine(destroyOnEnemy());
        }
        else if(collide.collider.CompareTag("solidObject")) {
            rigid = GetComponent<Rigidbody2D>(); 
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = 0;
            Destroy(GetComponent<BoxCollider2D>());
            StartCoroutine(destroyLaser());
        }
    }

    private IEnumerator destroyLaser()
    {
        laserAnim.SetBool("hit", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator destroyOnEnemy()
    {
        laserAnim.SetBool("hit", true);
        yield return new WaitForSeconds(0.2f);  
        Destroy(gameObject);
    }

}
