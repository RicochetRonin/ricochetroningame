using System.Collections.Generic;
using UnityEngine;

public class BulletVFXPool : MonoBehaviour
{
    public Stack<GameObject> inactive = new Stack<GameObject>();
    public GameObject parent;

    public BulletVFXPool(GameObject parent)
    {
        this.parent = parent;
    }
}