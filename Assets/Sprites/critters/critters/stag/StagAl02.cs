using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum State
{
    Idle,
    Moving,
}
[RequireComponent(typeof(NavMeshAgent))]

public class StagAl02 : MonoBehaviour
{
    [Header("Wander")]
    public float wanderDistance = 10f;
    public float walkSpeed = 5f;
    public float maxWalkTime = 6f;

    [Header("Idel")]
    public float idleTime = 5f;

    protected NavMeshAgent navMeshAgent;
    protected State currentState = State.Idle;

    private void Start()
    {
        InitailiseAnimal();
    }

    protected virtual void InitailiseAnimal()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = walkSpeed;

        currentState = State.Idle;
        UpdateState();
    }

    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Moving:
                HandheldMovingState();
                break;
        }
    }

    protected Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
        {
            return navMeshHit.position;

        }
        else
        {
            return GetRandomNavMeshPosition(origin, distance);
        }

    }

    protected virtual void HandleIdleState()
    {
        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToMove()
    {
        float waitTime = Random.Range(idleTime/2,idleTime * 2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomNavMeshPosition(transform.position, wanderDistance);
        navMeshAgent.SetDestination(randomDestination);
        SetState(State.Moving);

    }

    protected virtual void HandheldMovingState()
    {
        StartCoroutine(WaitToReachDestination());

    }

    private IEnumerator WaitToReachDestination()
    {
        float startTime  = Time.time;
        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (Time.time - startTime >= maxWalkTime)
            {
                navMeshAgent.ResetPath();
                SetState(State.Idle);
                yield break;

            }
            yield return null;
        }
        SetState(State.Idle);
    }

    protected void SetState(State newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged(newState);
    }

    protected virtual void OnStateChanged(State newState)
    {
        UpdateState();

    }



}
