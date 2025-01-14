using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    private void Start()
    {
        CreateUnitActionButtons();
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
    }

    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        ClearActionButtons();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform ActionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonTransform.GetComponent<ActionButtonUI>().SetBaseAction(baseAction);
        } 
    }

    private void ClearActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }
}
