using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    private List<ActionButtonUI> actionButtonUIList ;

    private void Awake()
    {
        actionButtonUIList = new();
    }
    private void Start()
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
    }

    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
    }
    private void OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void CreateUnitActionButtons()
    {
        ClearActionButtons();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform ActionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = ActionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            actionButtonUIList.Add(actionButtonUI);
        } 
    }

    private void ClearActionButtons()
    {
        actionButtonUIList.Clear();
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }

    private void UpdateSelectedVisual()
    {
        BaseAction baseAction = UnitActionSystem.Instance.GetSelectedAction();
        foreach (ActionButtonUI actionButton in actionButtonUIList)
        {
            actionButton.UpdateSelectedBorder();
        }
    }
}
