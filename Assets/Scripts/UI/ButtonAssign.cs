using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonAssign : MonoBehaviour
{
    private GameObject assignedVehiclePart;

    [SerializeField]private TextMeshProUGUI myText;
    [SerializeField]private TextMeshProUGUI myPrice;

    public void AssignPart(GameObject part)
    {
        assignedVehiclePart = part;

        UpdateSelf();
    }

    private void UpdateSelf()
    {
        GetComponent<Button>().onClick.AddListener(Custom_OnClickButton_ToggleSelector);
        myText.text = assignedVehiclePart.GetComponent<VehiclePart>().name;

        UpdatePrice();
    }

    public void UpdatePrice()
    {
        myPrice.text = "£" + assignedVehiclePart.GetComponent<VehiclePart>().CurrentPaint.PaintValue;
    }

    private void Custom_OnClickButton_ToggleSelector()
    {
        UIManager.Instance.MainSelector.gameObject.SetActive(true);
        UIManager.Instance.MainSelector.ToggleVisibility(this, assignedVehiclePart);
    }
}
