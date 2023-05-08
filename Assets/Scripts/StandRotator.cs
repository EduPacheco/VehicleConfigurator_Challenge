using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandRotator : MonoBehaviour
{
    Vector2 standRotation;

    [SerializeField] private float mouseSensitivity = .5f;

    private bool dragable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(dragable);
        if(!dragable) return;

        standRotation.x -= Input.GetAxis("Mouse X") * mouseSensitivity;
        standRotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity; 
        transform.localRotation = Quaternion.Euler( 0, standRotation.x, 0);
    }

    #region MOUSE METHODS
    private void OnMouseDown()
    {
        dragable = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMouseUp()
    {
        dragable = false;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion
}
