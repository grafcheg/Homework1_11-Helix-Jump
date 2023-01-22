using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Transform Level;
    public float Sensitivity = 0.25f;

    private Vector3 _previousMousePosition;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            Level.Rotate(0, -delta.x * Sensitivity, 0);
        }
        _previousMousePosition = Input.mousePosition;
    }
}
