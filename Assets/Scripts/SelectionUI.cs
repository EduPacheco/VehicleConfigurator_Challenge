using TMPro;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] private SelectionType selectionToChange;
    [SerializeField] private TextMeshProUGUI selectionDisplay;

    private int curSelection = 0;

    public void OnClick_Left()
    {
        curSelection = VehicleConfigurator.instance.CustomizeVehicle(selectionToChange, curSelection, -1);
        UpdateSelection();
    }

    public void OnClick_Right()
    {
        curSelection = VehicleConfigurator.instance.CustomizeVehicle(selectionToChange, curSelection, +1);
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        selectionDisplay.text = curSelection.ToString();
    }
}
