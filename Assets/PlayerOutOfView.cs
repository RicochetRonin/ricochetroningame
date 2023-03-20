using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerOutOfView : MonoBehaviour
{

    public delegate void OutOfView();
    public static event OutOfView outOfView;

    public CinemachineVirtualCamera vcam;
    private Camera cam;
    private Transform target;

    void Start()
    {
        cam = GetComponent<Camera>();
        target = vcam.LookAt;
        // vcam.transform.position = target.transform.position;
        // cam.transform.position = target.transform.position;
    }

    void Update()
    {
        Vector3 playerOnScreenPos = cam.WorldToScreenPoint(target.position);

        // if (playerOnScreenPos.x < 0 || playerOnScreenPos.x > Screen.width ||
        //     playerOnScreenPos.y < 0 || playerOnScreenPos.y > Screen.height) 
            // { outOfView?.Invoke(); }
         
    }

}
