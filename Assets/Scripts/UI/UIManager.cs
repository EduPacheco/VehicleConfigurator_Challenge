using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Control of User Interface Elements, supposedly...
/// </summary>

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [Header("Vehicle Selection")]
    [SerializeField] private GameObject vehicle0;
    [SerializeField] private GameObject vehicle1;
    [SerializeField] private GameObject vehicleStand;
    private GameObject currentVehicle;

    [Header("Top Side - Selector and PriceDisplay")]
    [SerializeField] private TextMeshProUGUI vehicleValue;
    [SerializeField] private TextMeshProUGUI valueShadow;
    [SerializeField] private SelectionUI mainSelector;
    public SelectionUI MainSelector { get => mainSelector; }


    [Header("Side Panel - Part Selector")]
    [SerializeField] private GameObject scrollViewSelectors;
    [SerializeField] private ButtonAssign prefabButton;
    [SerializeField] private ToggleAssign prefabToggle;

    [Header("Receipt")]
    [SerializeField] private GameObject receiptPanel;
    [SerializeField] private TextMeshProUGUI receiptText;

    private void Start()
    {
        mainSelector.gameObject.SetActive(false);
    }

    public void UpdateVehicleValue(int value)
    {
        vehicleValue.text = "£" + value;
        valueShadow.text = "£" + value;
    }

    public void OnClick_Vehicle0()
    {
        SpawnVehicle(vehicle0);
    }

    public void OnClick_Vehicle1()
    {
        SpawnVehicle(vehicle1);
    }

    private void SpawnVehicle(GameObject toSpawn)
    {
        if(currentVehicle != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        currentVehicle = (GameObject)Instantiate(toSpawn, vehicleStand.transform);
        CreateCustomizationUI();
    }

    private void CreateCustomizationUI()
    {
        foreach (GameObject carChild in VehicleConfigurator.Instance.AllModificationsList)
        {
            switch (carChild.GetComponent<VehiclePart>().PartType)
            {
                case VehiclePartType.CORE:
                    CreateButton(carChild);
                    break;
                case VehiclePartType.ADDON:
                    CreateToggle(carChild);
                    break;
            }
        }
    }

    private void CreateButton(GameObject part)
    {
        ButtonAssign b = Instantiate(prefabButton, scrollViewSelectors.transform);
        b.AssignPart(part);
    }

    private void CreateToggle(GameObject part)
    {
        ToggleAssign t = Instantiate(prefabToggle, scrollViewSelectors.transform);
        t.AssignPart(part);
    }

    public void OnClick_ReceiptDisplay()
    {
        receiptPanel.SetActive(!receiptPanel.activeSelf);
        receiptText.text = VehicleConfigurator.Instance.TotalVehicleValue();
    }

    public void OnClick_QuitApplication()
    {
        Application.Quit();
    }
}