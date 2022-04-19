using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    //We need a reference from our enemy to our player
    Transform target;

    //We need a reference to our MashAgent to move our enemy
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance between target (player) and transfor (The enemy)
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
             //start chasing the player
             agent.SetDestination(target.position);

            //Rotate the enemy towards the player (facing the player)
            if(distance <= agent.stoppingDistance)
            {
                //Attack the target

                //Face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(target.rotation,lookRotation, Time.deltaTime * 5f); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
