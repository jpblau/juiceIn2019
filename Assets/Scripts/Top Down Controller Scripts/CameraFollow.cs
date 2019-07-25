using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region public vars
    [Header("Target")]
    public Transform target;
    [Header("Distances")]
    public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public Vector3 offset;
    [Header("Speeds")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
        {
            Debug.Log("No target set for the camera!");
            return;
        }

        // Zoom
        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;


        // Movement
        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);

    }
}
