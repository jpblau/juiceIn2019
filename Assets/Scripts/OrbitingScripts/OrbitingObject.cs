using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrbitingObject : MonoBehaviour
{
    public float degreesPerSecond = 10.0f;
    public GameObject rotationRoot;

    private FollowingObject rootFollower;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        rootFollower = rotationRoot.GetComponent<FollowingObject>();

        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.destination != null && navMeshAgent.isStopped == false)
        {
            if (Vector3.Distance(transform.position, navMeshAgent.destination) <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.isStopped = true;
            }
        }

        /*
        // if we are not catching up to our player, rotate around them
        if (!rootFollower.GetIsFollowing())
        {
            // Spin the object around the root at the given degrees per second
            this.gameObject.transform.RotateAround(rotationRoot.transform.position, rotationRoot.transform.up, degreesPerSecond * Time.deltaTime);
        }
        */
    }


    public void AttackLocation(AnimationCurve curve, float deltaTime, Vector3 location)
    {
        Debug.Log("attacking location: " + location);

        // Make sure that our offset is correct
        //position.y += rootYOffset;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(location);
    }
}
