using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class StagAl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderDistance;
    [SerializeField] float wanderJitter;
    private Vector2 wanderTarget;
    public Vector2 dir;
    public Vector2 z ;

    [SerializeField] private float timeBtwStages = 1f;
    private float timer;

    private Animator Anim;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Anim = GetComponent<Animator>();
        
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    
    void Wander()
    {
        wanderTarget = this.transform.position;
        wanderTarget += new Vector2(Random.Range(-1.0f, 1.0f) * wanderJitter, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector2 targetLocal = wanderTarget + new Vector2(wanderDistance, 0);
        Vector2 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Debug.DrawRay(transform.position, targetWorld, Color.yellow);
        Seek(targetWorld);
    }

    void OnDrawGizmosSelected()
    {
   
        // Draw a rectangle to represent the area of random offsets
        Gizmos.color = Color.red; // Green color with some transparency
        //Gizmos.DrawCube(new Vector2(Random.Range(-1.0f, 1.0f) * wanderJitter, Random.Range(-1.0f, 1.0f) * wanderJitter), new Vector3(wanderJitter, wanderJitter));

       
    }

    private void Update()
    {
        Wander();
    }

}
