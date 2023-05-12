using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A more specific Class from the parent VehiclePart Class
/// </summary>
public class Addon : VehiclePart
{
    [SerializeField] private int addonPrice;
    public int AddonPrice { get => addonPrice; }
}