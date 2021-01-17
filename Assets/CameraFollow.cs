using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerToFollow;

    public float dist = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerToFollow)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = playerToFollow.position.x;
            newPosition.z = playerToFollow.position.z - dist;
            //newPosition.z = Mathf.Clamp(newPosition.z, farLeft.position.z, farRight.position.z);
            transform.position = newPosition;
        }
    }
}
