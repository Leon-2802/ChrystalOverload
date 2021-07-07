using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public Animator bulletAnim;
    public Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player")) {
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("arrow")) {
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("movingEnemy")) {
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("solidObject")) {
            rigid = GetComponent<Rigidbody2D>(); 
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = 0;
            Destroy(GetComponent<BoxCollider2D>());
            bulletAnim.SetTrigger("hit");
            StartCoroutine(destroyBullet());
        }
    }

    private IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
