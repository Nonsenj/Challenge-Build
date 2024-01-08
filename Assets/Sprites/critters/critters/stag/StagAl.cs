using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StagAl : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Seek(Vector2 location)
    {
        agent.SetDestination(location);

    }

    private void Update()
    {
        Seek(target.transform.position);
    }

}
