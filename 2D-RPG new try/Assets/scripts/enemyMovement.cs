using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform enemy;
    public Animator enemyAnim;
    NavMeshAgent agent;
    public mole1 script1;
    public Vector2 unpausedSpeed;
    public float detectRadius;
    public float stopRadius;
    public float attackRadius;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindWithTag("Player").transform;
        enemy = GameObject.FindWithTag("enemy").transform;

        script1 = GetComponent<mole1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script1.dontWalk == true)
        {
            agent.velocity = Vector2.zero;
            agent.isStopped = true;
        }
        else 
        {
            if (agent.isStopped) 
            {
               agent.isStopped = false;
               agent.velocity = unpausedSpeed;
            }
            agent.SetDestination(target.position);
            enemyAnim.SetFloat("horizontal", - (transform.position.x - target.position.x));
            enemyAnim.SetFloat("vertical", - (transform.position.y - target.position.y));
            enemyAnim.SetBool("walk", true);
            unpausedSpeed = agent.velocity;
        }
    }
}
