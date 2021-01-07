using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public Transform FirePointLeft;
    public Transform FirePointUp;
    public Transform FirePointDown;

    public GameObject arrowPrefab;
    public GameObject arrowLeftPrefab;
    public GameObject arrowUpPrefab;
    public GameObject arrowDownPrefab;

    public Animator animator;

    public float arrowSpeed = 30f;
    private Vector2 direction;
    bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && canAttack)
        {
                StartCoroutine(ShootCo());
        }
    }

    private IEnumerator ShootCo()
    {
        canAttack = false;
        if(Input.GetAxisRaw("Horizontal") == 1 || animator.GetFloat("LastHorizontal") > 0.01)
        {
            yield return new WaitForSeconds(0.3f);
            ShootRight();
            
        }
        else if(Input.GetAxisRaw("Horizontal") == -1 || animator.GetFloat("LastHorizontal") < 0)
        {
            yield return new WaitForSeconds(0.3f);
            ShootLeft();
        }
        else if(Input.GetAxisRaw("Vertical") == 1 || animator.GetFloat("LastVertical") > 0.01)
        {
            yield return new WaitForSeconds(0.3f);
            ShootUp();
        }
        else if(Input.GetAxisRaw("Vertical") == -1 || animator.GetFloat("LastVertical") < 0)
        {
            yield return new WaitForSeconds(0.3f);
            ShootDown();
        }
        canAttack = true;
    }

    void ShootRight()
    {
        GameObject arrow = Instantiate(arrowPrefab, FirePoint.position, FirePoint.rotation); //Spawnt den Pfeil am FirePoint
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>(); // Ein Rigidbody mit Name "rb" wird erstellt, welcher gleich mit dem des arrows ist
        rb.AddForce(new Vector2 (arrowSpeed, 0), ForceMode2D.Impulse); // Pfeil mit arrowSpeed nach rechts fliegen lassen

    }

    void ShootLeft()
    {
        GameObject arrowLeft = Instantiate(arrowLeftPrefab, FirePointLeft.position, FirePointLeft.rotation);
        Rigidbody2D rb = arrowLeft.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2 (-arrowSpeed, 0), ForceMode2D.Impulse);
    }

    void ShootUp()
    {
        GameObject arrowUp = Instantiate(arrowUpPrefab, FirePointUp.position, FirePointUp.rotation);
        Rigidbody2D rb = arrowUp.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2 (0, arrowSpeed), ForceMode2D.Impulse);
    }

    void ShootDown()
    {
        GameObject arrowDown = Instantiate(arrowDownPrefab, FirePointDown.position, FirePointDown.rotation);
        Rigidbody2D rb = arrowDown.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2 (0, -arrowSpeed), ForceMode2D.Impulse);
    }
}
