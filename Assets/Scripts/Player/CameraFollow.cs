using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public GameObject Leftwall;
    public GameObject RightWall;
    public Camera SceneCamera;
    private BoxCollider2D _LeftWallCollider;
    private BoxCollider2D _RightWallCollider;
    private float _LeftBound;
    private float _RightBound;
    private Vector3 _CurrentPos;
    private Vector3 velocity = Vector3.zero;
    private float _CameraExtent;

    private void Start()
    {
        _LeftWallCollider = Leftwall.GetComponent<BoxCollider2D>();
        _RightWallCollider = RightWall.GetComponent<BoxCollider2D>();

        _LeftBound = _LeftWallCollider.bounds.center.x - _LeftWallCollider.bounds.extents.x;
        _RightBound = _RightWallCollider.bounds.center.x + _RightWallCollider.bounds.extents.x;

        _CameraExtent = SceneCamera.GetComponent<Camera>().orthographicSize * SceneCamera.GetComponent<Camera>().aspect;
    }

    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.position;

        targetPosition.x = Mathf.Clamp(target.position.x, _LeftBound + _CameraExtent, _RightBound - _CameraExtent);
        targetPosition.z = transform.position.z;

        _CurrentPos = new Vector3(transform.position.x, 0, transform.position.z);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(_CurrentPos, targetPosition, ref velocity, smoothTime);
    }
}
