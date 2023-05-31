using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    GameObject player;
    ObjectPlacement objectPlacement;
    Vector3 position;
    public float rotationSpeed = 3.0f;
    public int seconds = 10;

    void Start() {        
        player = GameObject.FindGameObjectWithTag("Player");
        objectPlacement = player.GetComponent<ObjectPlacement>();
    }
    
    void Update()
    {
        position = objectPlacement.position;

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 followingPosition = new Vector3(
            cameraPosition.x + 1, cameraPosition.y - 0.8f, cameraPosition.z + 1.5f
        );

        transform.position = followingPosition + Camera.main.transform.forward * 4f;

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.LookRotation(position - transform.position), 
            rotationSpeed * Time.deltaTime
        );
    }
}