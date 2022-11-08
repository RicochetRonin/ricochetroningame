using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    public Stack<GameObject> inactive = new Stack<GameObject>();
    public GameObject parent;

    public BulletPool(GameObject parent)
    {
        this.parent = parent;
    }
}
