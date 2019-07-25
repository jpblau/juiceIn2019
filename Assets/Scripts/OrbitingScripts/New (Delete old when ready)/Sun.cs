using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    public GameObject leader;
    public float followRadius = 1.0f;   // The radius at which this object will begin following the leader
    public float speed = 2.0f;  // The speed at which the sun follows the leader

    private Transform leaderTrans;
    private float distance;
    private bool isFollowing = false;

    public float timeBetweenDirectionChanges = 0.5f;
    private float changedRecentlyTimer = 0.0f;
    private bool hasChangedDirectionRecently = false;

    public float orbitRadius = 0.5f;    // The radius at which the orbiting objects are allowed to rotate
    
    
    // Start is called before the first frame update
    void Start()
    {
        leaderTrans = leader.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update our changedRecentlyTimer
        changedRecentlyTimer += Time.deltaTime;
        if (changedRecentlyTimer > timeBetweenDirectionChanges)
        {
            hasChangedDirectionRecently = false;
            changedRecentlyTimer = 0.0f;
        }

        // Calculate our distance to our leader
        distance = Vector3.Distance(leaderTrans.position, this.transform.position);

        // Update isFollowing based on that distance relative to the follow radius
        if (distance > followRadius)
        {
            isFollowing = true;
        }

        if (distance <= 0.01f)
        {
            isFollowing = false;
        }

        // if our angle between the leader and this object is too different, let's swap what direction we're facing
        float angle = Vector3.Angle(transform.forward, leaderTrans.forward);
        if (isFollowing && (angle > 70.0f || angle < -70.0f) && !hasChangedDirectionRecently && !(distance < orbitRadius))
        {
            transform.forward = -transform.forward;
            hasChangedDirectionRecently = true;
        }

        // Update our position and forward vectors
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, leaderTrans.position, speed * Time.deltaTime);

            if (!(distance < orbitRadius))
            {
                transform.forward = Vector3.Lerp(transform.forward, leaderTrans.forward, 2 * Time.deltaTime);
            }
        }

        // if the sun is within the orbit radius, rotate the sun so that the planet origins orbit 
        if (distance < orbitRadius)
        {
            this.gameObject.transform.RotateAround(leaderTrans.position, leaderTrans.up, 60 * Time.deltaTime);
        }
    }

    
}
