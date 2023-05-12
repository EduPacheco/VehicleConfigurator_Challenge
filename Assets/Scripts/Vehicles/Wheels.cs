using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A more specific Class from the parent VehiclePart Class
/// </summary>
public class Wheels : VehiclePart
{
    [SerializeField] private int wheelsSetPrice;
    public int WheelsSetPrice { get => wheelsSetPrice; }
}