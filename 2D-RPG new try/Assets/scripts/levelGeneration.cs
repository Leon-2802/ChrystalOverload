using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    private int direction;
    public float moveAmount;
    public float moveAmountUp;

    public float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    public bool stopGeneration = false;

    public float minX;
    public float maxX;
    public float maxY;
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);   
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update() 
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) //Move Right
        {
            if (transform.position.x < maxX) {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Move Left
        {
            if (transform.position.x > minX) {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 5) //Move Up
        {
            if (transform.position.y < maxY) {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountUp);
                transform.position = newPos;
            }
            else {
                //Stop level Generation
                stopGeneration = true;
            }
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }
}
