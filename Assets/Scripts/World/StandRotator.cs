using UnityEngine;

/// <summary>
/// Rotates the vehicles stand when holding and draging with left mouse button
/// </summary>

public class StandRotator : MonoBehaviour
{
    Vector2 standRotation;

    [SerializeField] private float rotSpeed = .5f;
    [SerializeField] private int framesTillDrag = 30;

    private int framesCounter; 
    private bool dragable = false;

    /// <summary>
    /// If Holding LMB can drag mouse to rotate vehicle stand
    /// </summary>
    private void Update()
    {
        if (!dragable) return;

        standRotation.x -= Input.GetAxis("Mouse X") * rotSpeed;
        transform.localRotation = Quaternion.Euler(0, standRotation.x, 0);

    }

    #region MOUSE METHODS
    /// <summary>
    /// Resets drag
    /// </summary>
    private void OnMouseDown()
    {
        framesCounter = 0;
    }

    /// <summary>
    /// While holding LMB, start countdown, lock and hide mouse
    /// </summary>
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

    /// <summary>
    /// Unlockes mouse and cancels dragging
    /// </summary>
    private void OnMouseUp()
    {
        dragable = false;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion
}
