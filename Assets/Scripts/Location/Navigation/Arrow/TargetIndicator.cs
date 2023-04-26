using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public GameObject prefab;
    public float rotationSpeed = 3.0f;
    
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 followingPosition = new Vector3(
            cameraPosition.x, cameraPosition.y - 1f, cameraPosition.z + 1.5f
        );

        transform.position = followingPosition + Camera.main.transform.forward * 4f;

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.LookRotation(prefab.transform.position - transform.position), 
            rotationSpeed * Time.deltaTime
        );
    }
}
