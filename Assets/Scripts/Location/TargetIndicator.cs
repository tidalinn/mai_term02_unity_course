using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 arrowScale = new Vector3(0.8f, 0.7f, 1f);
    public float rotationSpeed = 3.0f;
    public int seconds = 10;
    GameObject player;
    ObjectPlacement objectPlacement;
    Vector3 position;
    ObjectGEO objectGEO;

    void Start() 
    {        
        player = GameObject.FindGameObjectWithTag("Player");
        objectPlacement = player.GetComponent<ObjectPlacement>();

        objectGEO = prefab.GetComponent<ObjectGEO>();

        transform.localScale = new Vector3(0, 0, 0);
    }
    
    void Update()
    {
        if (objectGEO.Latitude == 0 && objectGEO.Longitude == 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localScale = arrowScale;
            position = objectPlacement.position;

            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 followingPosition = new Vector3(
                cameraPosition.x + 1, cameraPosition.y, cameraPosition.z + 1.5f
            );

            transform.position = followingPosition + Camera.main.transform.forward * 4f;

            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.LookRotation(position - transform.position), 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}