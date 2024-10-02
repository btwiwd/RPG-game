using UnityEngine;
using System.Collections;

/// <summary>
/// CameraFollow script.
/// This is used for Main Camera to follow Player character.
/// </summary>
public class CameraRig : MonoBehaviour
{
    // Target tranform to follow.
	public Transform target;
    // for draw gizmos: draw line between camerarig and camera.
    public Camera mainCamera;

    // Follow delay time used Vector3.SmoothDamp function.
    public float smoothTime = 0.2f;

    // follow velocity used Vector3.SmoothDamp function.
    // Initial value = Vector3.zero.
    private Vector3 _currentVelocity = Vector3.zero;

    void Awake()
    {
        // If Follow Target is null, find target to Follow (Player).
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        // get main camera component.
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // let camera follow player little bit slowly using smooth damp function.
        transform.position 
            = Vector3.SmoothDamp(transform.position, target.position, ref _currentVelocity, smoothTime);
    }

    // Draw Gizmo Line between Camera Rig Position and Main Camera Position.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, mainCamera.transform.position);
    }
}