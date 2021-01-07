using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Rigidbody2D rigid;
    private Vector2 movement;
        
    void OnCollisionEnter2D(Collision2D collide)
    {
        if(collide.collider.CompareTag("Player")) {
            return;
        }
        rigid = GetComponent<Rigidbody2D>(); 
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        Destroy(GetComponent<BoxCollider2D>());
        StartCoroutine(Disappear());
    } 

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
