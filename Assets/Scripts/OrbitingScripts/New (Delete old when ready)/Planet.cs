using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Planet : MonoBehaviour
{
    public GameObject home;

    private enum PlanetState { Orbiting, Attacking };
    PlanetState planetState;

    private Sun sun;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        planetState = PlanetState.Orbiting;

        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.destination = home.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.destination != null && navMeshAgent.isStopped == false)
        {
            if (Vector3.Distance(transform.position, navMeshAgent.destination) <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.isStopped = false;
            }
        }
    }

    /// <summary>
    /// Have a planet attack by moving to the given location
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="deltaTime"></param>
    /// <param name="location"></param>
    public void AttackLocation(AnimationCurve curve, float deltaTime, Vector3 location)
    {
        Debug.Log("Attacking Location: " + location);

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(location);

        planetState = PlanetState.Attacking;
    }
}
