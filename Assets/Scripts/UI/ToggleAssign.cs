using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// User Interface Element
/// </summary>

public class ToggleAssign : MonoBehaviour
{
    private GameObject assignedVehiclePart;

    private Toggle t;

    [SerializeField]private TextMeshProUGUI myText;
    [SerializeField]private TextMeshProUGUI myPrice;

    public void AssignPart(GameObject part)
    {
        assignedVehiclePart = part;
        
        UpdateSelf();
    }

    private void UpdateSelf()
    {
        t = GetComponent<Toggle>();

        t.isOn = assignedVehiclePart.activeSelf;

        myText.text = assignedVehiclePart.GetComponent<Addon>().name;
        t.onValueChanged.AddListener(delegate
        {
            Custom_OnValueChange_ToggleAddon(t);
        });

        UpdatePrice();
    }

    private void UpdatePrice()
    {
        Addon a = assignedVehiclePart.GetComponent<Addon>();

        if (t.isOn)
            myPrice.text = "£" + a.AddonPrice;
        else
            myPrice.text = "£" + 0;     
    }

    private void Custom_OnValueChange_ToggleAddon(Toggle change)
    {
        VehicleConfigurator.Instance.CustomizeVehicle(assignedVehiclePart, t.isOn);

        UpdatePrice();
    }
}
