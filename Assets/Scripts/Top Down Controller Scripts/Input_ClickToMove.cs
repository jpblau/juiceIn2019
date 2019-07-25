using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Input_ClickToMove : MonoBehaviour
{
    public Terrain goTerrain;
    public float speed;

    public float rootYOffset;

    private enum InputType { Terrain, Enemy, Object };
    InputType userInput;

    private NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        if (!navMeshAgent.isStopped)
        {
            switch (userInput)
            {
                case InputType.Terrain:
                    float step = speed * Time.deltaTime;
                    // If our destination isn't null
                    if (navMeshAgent.destination != null)
                    {
                        if (Vector3.Distance ( transform.position, navMeshAgent.destination) <= navMeshAgent.stoppingDistance)
                        {
                            navMeshAgent.isStopped = true;
                        }
                    }
                    break;
                case InputType.Enemy:
                    break;
                case InputType.Object:
                    break;
            }
        }
    }

    /// <summary>
    /// Determines how the user's input should be processed
    /// </summary>
    private void HandleClick()
    {
        // Get the position of the mouse on the terrain
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If we hit a spot on the terrain, let's move to that spot
        if (goTerrain.GetComponent<Collider>().Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            userInput = InputType.Terrain;
            MoveToPosition(hit.point);
        }

        // If we hit an enemy, let's move into attack range with that enemy

        // If we hit an interactive object, let's move to that interactive object
    }

    /// <summary>
    /// Moves the actor to the given position. The position should be a location on the terrain
    /// </summary>
    /// <param name="position">The new position</param>
    private void MoveToPosition(Vector3 position)
    {
        // Make sure that our offset is correct
        position.y += rootYOffset;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(position);
        Debug.Log(position);
        
    }
}
