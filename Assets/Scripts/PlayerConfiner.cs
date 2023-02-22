using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Cannot use the existing polygon collider to keep the player in bounds of the camera. This script is used to create an edge collider from the camera confiner (polygon collider), which can keep the player in bounds
public class PlayerConfiner : MonoBehaviour
{
    public PolygonCollider2D poly;
    private EdgeCollider2D edge;

    // Used https://www.youtube.com/watch?v=NbvcfMjAlQ4 for code

    void Awake()
    {
        edge = GetComponent<EdgeCollider2D>();
        Vector2[] points = poly.points;
        edge.points = points;
    }

}
