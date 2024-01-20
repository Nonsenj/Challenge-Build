using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BadgerAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject Player;

    public float EnemyDistaneRun = 4.0f;
    private Animator Anim;
    public Vector3 dir;




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Anim.SetBool("isRunning", false);
        if (distance < EnemyDistaneRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 nowPos = transform.position + dirToPlayer;
            dir = Player.transform.position - transform.position;
            Anim.SetBool("isRunning", true);
            agent.SetDestination(nowPos);

        }
        Anim.SetFloat("AimX", dir.x);
        Anim.SetFloat("AimY", dir.y);
    }
}
