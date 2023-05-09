using System.Collections.Generic;
using UnityEngine;

public enum SelectionType
{
    TOP,
    BOTTOM,
    ADDON
}

public class VehicleConfigurator : MonoBehaviour
{
    #region SINGLETON
    public static VehicleConfigurator instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region VARIABLES
    [SerializeField] private GameObject vehicleAddons;
    private List<GameObject> addons = new List<GameObject>();

    [SerializeField] private GameObject[] paintableVehicleParts;

    [SerializeField] private Material[] paints;

    [SerializeField] private int vehicleBaseValue;
    private int vehicleCurrentValue;
    #endregion

    private void Start()
    {
        foreach (Transform child in vehicleAddons.transform)
        {
            addons.Add(child.gameObject);
        }
    }

    public int CustomizeVehicle(SelectionType pieceToCustomize, int currentPiece, int newPiece)
    {
        switch (pieceToCustomize)
        {
            case SelectionType.TOP:
                newPiece = LoopAroundArray(paints.Length, currentPiece, newPiece);
                paintableVehicleParts[0].GetComponent<Renderer>().sharedMaterial = paints[newPiece];
                break;

            case SelectionType.BOTTOM:
                newPiece = LoopAroundArray(paints.Length, currentPiece, newPiece);
                paintableVehicleParts[1].GetComponent<Renderer>().sharedMaterial = paints[newPiece];
                break;

            case SelectionType.ADDON:
                newPiece = LoopAroundArray(addons.Count, currentPiece, newPiece);
                addons[currentPiece].SetActive(false);
                addons[newPiece].SetActive(true);
                break;
        }

        return newPiece;
    }

    private int LoopAroundArray(int arraySize, int curP, int newP)
    {
        if(curP + newP >= arraySize) return 0;
        if(curP + newP < 0) return arraySize - 1;
        return curP + newP;
    }
}