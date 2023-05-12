using UnityEngine;

/// <summary>
/// Paint script with basic information, 
/// Next Improvement -> could evolve into a scriptable object, it does not have to be an object by itself
/// </summary>
public class Paint : MonoBehaviour
{
    [SerializeField] private string paintName;
    public string PaintName { get => paintName; }

    [SerializeField] private int paintValue;
    public int PaintValue { get => paintValue; }

    [SerializeField] private Material myMat;
    public Material MyMat { get => myMat; }

}