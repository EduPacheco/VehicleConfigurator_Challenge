using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Vehicle Editor
/// </summary>

public enum VehiclePartType
{
    CORE,
    WHEELS,
    ADDON
}

public class VehicleConfigurator : MonoBehaviour
{
    #region SINGLETON
    public static VehicleConfigurator Instance { get; private set; }

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

    #region VARIABLES
    /// <summary>
    /// List of all customizable components of the vehicle
    /// </summary>
    [SerializeField] private List<GameObject> allModificationsList;
    public List<GameObject> AllModificationsList { get => allModificationsList; }

    /// <summary>
    ///  Array of available paints to customize the vehicle
    /// </summary>
    [SerializeField] private Paint[] availablePaints;

    [SerializeField] private int vehicleBaseValue = 999;
    private int vehicleCurrentValue;

    #endregion

    private void Start()
    {
        InitialEvaluation();
    }

    /// <summary>
    /// Customizes given vehicle part
    /// </summary>
    /// <param name="partToCustomize"></param>
    /// <param name="arrayOffsetNewPart"></param>
    /// <returns></returns>
    public int CustomizeVehicle(GameObject partToCustomize, int arrayOffsetNewPart)
    {
        VehiclePart part = partToCustomize.GetComponent<VehiclePart>();

        int cur = System.Array.IndexOf(availablePaints, part.CurrentPaint);

        int newPaint = LoopAroundArray(availablePaints.Length, cur, arrayOffsetNewPart);

        Paint oldPaint = part.PaintPart(availablePaints[newPaint]);

        ValueChange(-oldPaint.PaintValue);
        ValueChange(part.CurrentPaint.PaintValue);

        VehicleEvaluation();

        return part.CurrentPaint.PaintValue;
    }

    /// <summary>
    /// Toggles Modification on the vehicle
    /// </summary>
    /// <param name="partToCustomize">MOdification to install of remove from the vehicle</param>
    /// <param name="toggleInstallPart">To toggle the Modification On or Off</param>
    /// <returns></returns>
    public int CustomizeVehicle(GameObject partToCustomize, bool toggleInstallPart)
    {
        partToCustomize.SetActive(toggleInstallPart);

        Addon a = partToCustomize.GetComponent<Addon>();
        if (a != null)
        {
            if (partToCustomize.activeSelf)
                ValueChange(a.AddonPrice);
            else
                ValueChange(-a.AddonPrice);
        }

        VehicleEvaluation();

        if (partToCustomize.activeSelf)
            return a.AddonPrice;
        
        return 0;
    }

    /// <summary>
    /// Checks if the next index to be used is within the bounds of the array
    /// Loops around the array if it goes outside of Bounds
    /// </summary>
    /// <param name="arraySize">Array length to check</param>
    /// <param name="curIndex">Current array index</param>
    /// <param name="indexOffset">Offset to check if within Bounds</param>
    /// <returns>Clamped and Looped Index Value</returns>
    private int LoopAroundArray(int arraySize, int curIndex, int indexOffset)
    {
        if (curIndex + indexOffset >= arraySize) return 0;
        if (curIndex + indexOffset < 0) return arraySize - 1;
        return curIndex + indexOffset;
    }

    /// <summary>
    /// Update Vehicle price
    /// </summary>
    /// <param name="add">Part price to add</param>
    private void ValueChange(int add)
    {
        vehicleCurrentValue += add;
    }

    /// <summary>
    /// Initial vehicle price evaluation
    /// </summary>
    private void InitialEvaluation()
    {
        ValueChange(vehicleBaseValue);

        foreach (GameObject part in AllModificationsList)
        {
            switch (part.GetComponent<VehiclePart>().PartType)
            {
                case VehiclePartType.CORE:
                    ValueChange(part.GetComponent<VehiclePart>().CurrentPaint.PaintValue);
                    break;
                case VehiclePartType.ADDON:
                    if(part.activeSelf) ValueChange(part.GetComponent<Addon>().AddonPrice);
                    break;
            }
        }
        VehicleEvaluation();
    }

    /// <summary>
    /// Communicates with the UIManager to Update the vehicle total value on screen Display
    /// </summary>
    public void VehicleEvaluation()
    {
        UIManager.Instance.UpdateVehicleValue(vehicleCurrentValue);
    }

    /// <summary>
    /// Writes a detailed string of each vehicle customization with name, prices and display total price
    /// </summary>
    /// <returns>Detailed price receitp</returns>
    public string TotalVehicleValue()
    {
        string receipt = "";

        receipt += "Base Vehicle Value: £" + vehicleBaseValue;

        receipt += "\n\nCustomization to vehicle parts:";
        foreach (GameObject part in AllModificationsList)
        {
            if(part.GetComponent<VehiclePart>().PartType == VehiclePartType.CORE)
            {
                receipt += "\n\t" + part.GetComponent<VehiclePart>().CurrentPaint.name
                    + " on " + part.GetComponent<VehiclePart>().name + ": £"
                    + part.GetComponent<VehiclePart>().CurrentPaint.PaintValue;
            }
        }

        receipt += "\n\nAdditional Modifications to vehicle:";
        foreach (GameObject part in AllModificationsList)
        {
            if (part.GetComponent<VehiclePart>().PartType == VehiclePartType.ADDON
                && part.activeSelf)
            {
                receipt += "\n\t" + part.GetComponent<Addon>().name + ": £"
                    + part.GetComponent<Addon>().AddonPrice;
            }
        }

        receipt += "\n\n\nTOTAL TO PAY: £" + vehicleCurrentValue;

        return receipt;
    }
}