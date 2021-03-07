using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    private int direction;
    private int rand;
    public float moveAmount;
    public float moveAmountUp;

    public float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    public bool stopGeneration = false;
    public spawnBoss bossScript;

    public float minX;
    public float maxX;
    public float maxY;
    public LayerMask room;
    private int downCounter;
    public int rowCounter = 0;
    public spawnPlayer spawnPly;

    void Start()
    {
        int randStartingPos = 1;
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[3], transform.position, Quaternion.identity);
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
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3) {
                    direction = 2;
                }
                else if (direction == 4) {
                    direction = 5;
                }
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Move Left
        {
            downCounter = 0;

            if (transform.position.x > minX) {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 5) //Move Up
        {
            downCounter++;
            rowCounter++;

            if (transform.position.y < maxY) 
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<roomType>().type != 1 && roomDetection.GetComponent<roomType>().type != 3) {

                    if (downCounter >= 2) {
                        roomDetection.GetComponent<roomType>().destroyRoom();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else {
                        roomDetection.GetComponent<roomType>().destroyRoom();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2) {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountUp);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else {
                //Stop level Generation
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<roomType>().type != 3) {
                    roomDetection.GetComponent<roomType>().destroyRoom();
                    GameObject lastRoom = Instantiate(rooms[3], transform.position, Quaternion.identity);
                    bossScript = lastRoom.GetComponent<spawnBoss>();
                    bossScript.spawn();
                }
                stopGeneration = true;
            }
        }
    }
}
