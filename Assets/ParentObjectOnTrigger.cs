using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParentObjectOnTrigger : MonoBehaviour
{
    [SerializeField] private string tagToCompare;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagToCompare))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagToCompare))
        {
            other.transform.parent = null;
        }
    }
}
