using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public List<GameObject> orbitingObjects;
    public AnimationCurve attackCurve;

    private Vector3 clickedPosition = Vector3.zero;
    private GameObject closestGO;

    public Terrain goTerrain;
    

    // Start is called before the first frame update
    void Start()
    {
        closestGO = orbitingObjects[0];



        
    }

    // Update is called once per frame
    void Update()
    {
        // When our user right clicks
        if (Input.GetMouseButtonDown(1))
        {
            // Get the position on the navmesh that we want to move to. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If we hit a spot on the terrain, let's move to that spot
            if (goTerrain.GetComponent<Collider>().Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                clickedPosition = hit.point;
            }

            float minDistance = 1000000.0f;

            // Get the orbitingObject that is closest to that point
            foreach (GameObject go in orbitingObjects)
            {
                float newDistance = Vector3.Distance(go.transform.position, clickedPosition);
                if (newDistance < minDistance)
                {
                    closestGO = go;
                }
            }

            // Tell that orbiting object to move towards a location
            closestGO.GetComponent<OrbitingObject>().AttackLocation(attackCurve, Time.deltaTime, clickedPosition);
        }

       
    }
}
