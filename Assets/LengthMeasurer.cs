using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LengthMeasurer : MonoBehaviour
{

    [SerializeField] private Vector2 startPos, endPos;

    [SerializeField] private Color color;

    [SerializeField] private int fontSize;
    //[SerializeField] private string textToDisplay;
    
    private float _length;
    
    private GUIStyle guiStyle = new GUIStyle();
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawLine(startPos, endPos);

        MeasureDistance();
        guiStyle.normal.textColor = color;
        guiStyle.fontSize = fontSize;
        Handles.Label(startPos, "Length: " + _length, guiStyle);
        Handles.Label(endPos, "Length: " + _length, guiStyle);
    }

    void MeasureDistance()
    {
        _length = Vector2.Distance(startPos, endPos);
    }
}
