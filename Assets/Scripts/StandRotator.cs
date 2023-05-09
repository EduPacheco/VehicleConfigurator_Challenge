using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandRotator : MonoBehaviour
{
    Vector2 standRotation;

    [SerializeField] private float rotSpeed = .5f;
    [SerializeField] private int framesTillDrag = 30;

    private int framesCounter; 
    private bool dragable = false;

    private void Update()
    {
        Debug.Log(dragable);

        if (!dragable) return;

        standRotation.x -= Input.GetAxis("Mouse X") * rotSpeed;
        transform.localRotation = Quaternion.Euler(0, standRotation.x, 0);

    }

    #region MOUSE METHODS
    private void OnMouseDown()
    {
        Debug.Log("Clicked");

        framesCounter = 0;
    }

    private void OnMouseDrag()
    {
        if(framesCounter < framesTillDrag)
        {
            framesCounter++;
            return;
        }

        if (!dragable) dragable = true;

        if(Cursor.lockState != CursorLockMode.Locked) 
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMouseUp()
    {
        dragable = false;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion
}
