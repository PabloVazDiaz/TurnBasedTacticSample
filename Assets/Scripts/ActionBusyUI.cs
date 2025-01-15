using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private GameObject BusyVisual;

    void Start()
    {
        UpdateBusyVisual(false);
        UnitActionSystem.Instance.onBusyChanged += UpdateBusyVisual;
    }


    public void UpdateBusyVisual(bool isActive)
    {
        BusyVisual.SetActive(isActive);
    }
}
