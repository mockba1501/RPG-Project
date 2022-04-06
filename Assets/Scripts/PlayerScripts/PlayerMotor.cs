using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;        //Target to follow
    NavMeshAgent agent;     //Reference to our agent

    //Initializing
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        //To ensure the player stops within a reasonable distance from the interactable
        agent.stoppingDistance = newTarget.radius * 0.8f;
        
        //To correct the rotation of the player while inside the radius of the interactable
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        //Reset everything
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null; 
    }

    void FaceTarget()
    {
        //Get the direction from our target position to our current position
        Vector3 direction = (target.position - transform.position).normalized;
        
        //To ensure that the player is not looking up and down only right/left
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0f, direction.z));

        //To ease and smooth the rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * 5f);
    }
}
