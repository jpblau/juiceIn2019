using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    public GameObject leader;
    public float followRadius = 1.0f;    // The radius at which this object will begin following the leader
    public float followSpeed = 2.0f;   // The speed at which this object will follow the leader

    private Transform leaderTrans;
    private float distance;
    private bool isFollowing = false;

    private bool hasChangedDirectionRecently = false;
    private float changedRecentlyTimer = 0.0f;
    public float timeBetweenDirectionChanges = 0.5f;

    public float orbitRadius = .5f; // The range at which the orbiting objects are allowed to rotate. Ideally, is less than the followRadius



    public GameObject root;

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

        // if our angle between the root and this object is too different, let's swap what direction we're facing
        float angle = Vector3.Angle(transform.forward, root.transform.forward);
        if (isFollowing && (angle > 70.0f || angle < -70.0f) && !hasChangedDirectionRecently && !(distance < orbitRadius))
        {
            transform.forward = -transform.forward;

            // We have changed directions recently
            hasChangedDirectionRecently = true;
        }

        // If we are following the object, update our position and forward vector accordingly
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, leaderTrans.position, followSpeed * Time.deltaTime);
            
            if (!(distance < orbitRadius))
            {
                transform.forward = Vector3.Lerp(transform.forward, leaderTrans.forward, 2 * Time.deltaTime);
            }
            

            // if we are following, make sure our forward vectors are aligned
            //this.transform.forward = leaderTrans.forward;
        }

        // Have our objects orbit if they are within the orbitRadius
        if (distance < orbitRadius)
        {
            this.gameObject.transform.RotateAround(root.transform.position, root.transform.up, 60 * Time.deltaTime);
        }

    }

    /// <summary>
    /// A getter for isFollowing
    /// </summary>
    /// <returns>isFollowing, which is true if this object is moving ("following") the object, false otherwise</returns>
    public bool GetIsFollowing()
    {
        return isFollowing;
    }
}
