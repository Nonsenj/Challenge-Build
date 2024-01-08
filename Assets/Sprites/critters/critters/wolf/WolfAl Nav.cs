using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAlNav : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public bool shouldRotate;
    private Animator Anim;
    public Vector3 dir;

    private bool Isstop;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Seek(Vector2 location)
    {
        dir = target.transform.position - transform.position;
        if (shouldRotate)
        {
            Anim.SetFloat("AimX", dir.x);
            Anim.SetFloat("AimY", dir.y);
        }
        agent.SetDestination(location);
        if (Vector3.Distance(transform.position, target.transform.position) < 2)
        {
            Anim.SetBool("isRunning", false);
        }
        else
        {
            Anim.SetBool("isRunning", true);
        }

    }

    private void Update()
    {

        Seek(target.transform.position);

        
    }
}
