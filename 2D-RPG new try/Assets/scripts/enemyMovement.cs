using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMovement : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    Path path;
    int currenWayPoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
