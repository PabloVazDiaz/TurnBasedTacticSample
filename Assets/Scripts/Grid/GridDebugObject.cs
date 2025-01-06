using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    private void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }

}
