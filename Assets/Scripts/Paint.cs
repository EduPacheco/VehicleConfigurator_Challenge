using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private string paintName;
    public string PaintName { get => paintName; }

    [SerializeField] private int paintValue;
    public int PaintValue { get => paintValue; }

    private Material myMat;
    public Material MyMat { get => myMat; }

    void Start()
    {
        myMat = GetComponent<MeshRenderer>().sharedMaterial;
    }
}