using TMPro;
using UnityEngine;

/// <summary>
/// User Interface Element to select from an array of vehicle parts (in this case, array of paints paints)
/// </summary>
public class SelectionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI selectionDisplay;
    [SerializeField] private TextMeshProUGUI priceDisplay;

    private GameObject currentPart = null;
    private ButtonAssign currentButton = null;

    public void OnClick_Left()
    {
        VehicleConfigurator.Instance.CustomizeVehicle(currentPart, -1);
        UpdateSelection();
    }

    public void OnClick_Right()
    {
        VehicleConfigurator.Instance.CustomizeVehicle(currentPart, +1);
        UpdateSelection();
    }

    /// <summary>
    /// When changing, Updates Name of the part, and price
    /// </summary>
    private void UpdateSelection()
    {
        string part = currentPart.GetComponent<VehiclePart>().name;
        string paint = currentPart.GetComponent<VehiclePart>().CurrentPaint.name;
        string price = currentPart.GetComponent<VehiclePart>().CurrentPaint.PaintValue.ToString();

        selectionDisplay.text = paint + " " + part;
        priceDisplay.text = "£" + price;
        currentButton.UpdatePrice();
    }

    /// <summary>
    /// Check if it is in use (active), if so update to new part to be customized
    /// If it is used by the same button twice it will hide itself
    /// </summary>
    /// <param name="button">Button that requested the use of Selection</param>
    /// <param name="newPart">New part to be customized</param>
    public void ToggleVisibility(ButtonAssign button, GameObject newPart)
    {
        if(newPart == currentPart)
        {
            gameObject.SetActive(false);
            return;
        }

        currentPart = newPart;
        currentButton = button;
        UpdateSelection();
    }

    private void OnDisable()
    {
        currentPart = null;
        currentButton = null;
    }
}
