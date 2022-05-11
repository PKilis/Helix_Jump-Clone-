using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject objectToFollow;
    Vector3 offset;
    Vector3 camPos;

    void Start()
    {
        objectToFollow = GameObject.FindWithTag("Ball");
        offset = transform.position - objectToFollow.transform.position;
    }


    void Update()
    {
       camPos  = offset + objectToFollow.transform.position;
       transform.position = Vector3.Lerp(transform.position, camPos, 2);
    }
}
