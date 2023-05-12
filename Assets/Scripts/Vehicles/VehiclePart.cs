using UnityEngine;

/// <summary>
/// Parent Vehicle Part
/// </summary>
public class VehiclePart : MonoBehaviour
{
    [SerializeField] private VehiclePartType partType;
    public VehiclePartType PartType { get => partType;}

    [SerializeField] private string partName;
    public string PartName { get => partName;}
    
    [SerializeField] private bool paintablePart;
    public bool PaintablePart { get => paintablePart;}

    [SerializeField]private Paint currentPaint;
    public Paint CurrentPaint { get => currentPaint; }

    /// <summary>
    /// Updates Part with Initial information, in this case, just initial paint
    /// </summary>
    private void Start()
    {
        PaintPart(currentPaint);
    }

    /// <summary>
    /// Paints Part with a new Paint
    /// </summary>
    /// <param name="newPaint">New paint to be changed into</param>
    /// <returns>Old paint, previous in use</returns>
    public Paint PaintPart(Paint newPaint)
    {
        Paint oldPaint = currentPaint;

        currentPaint = newPaint;

        if (paintablePart)
            GetComponent<MeshRenderer>().sharedMaterial = currentPaint.GetComponent<MeshRenderer>().sharedMaterial;

        return oldPaint;
    }
}