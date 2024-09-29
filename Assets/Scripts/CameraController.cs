using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float Min = 0.5f;
    public float Max = 3f;
    public float speed = 10f;
    public Vector3 direction;
    public float CurrentDistance;

    void Start()
    {
        direction = transform.localPosition.normalized;
        CurrentDistance = transform.localPosition.magnitude;
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        Vector3 NextCameraPose = player.TransformPoint(direction*Max);
        RaycastHit Hit;
        if(Physics.Linecast(player.position, NextCameraPose, out Hit))
        {
            CurrentDistance = Mathf.Clamp(Hit.distance, Min, Max);
        }
        else
        {
            CurrentDistance = Max;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, direction*CurrentDistance, Time.deltaTime*speed);
        transform.localPosition = new Vector3(transform.localPosition.x, 1.6f, transform.localPosition.z);
    }
}
