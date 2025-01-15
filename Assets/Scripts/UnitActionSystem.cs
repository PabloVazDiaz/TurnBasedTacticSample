using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public delegate void OnBusyChanged(bool isBusy);
    public OnBusyChanged onBusyChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitPlaneMask;

    private BaseAction selectedAction;
    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        if (isBusy)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (!HandleUnitSelection())
            {
                HandleSelectedAction();
            }
        }
    }

    private bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitPlaneMask))
        {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if(unit == selectedUnit)
                {
                    //Unit already selected
                    return false;
                }
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void HandleSelectedAction()
    {
        switch (selectedAction)
        {
            case MoveAction moveAction:
                {
                    GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
                    if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        selectedUnit.GetAction<MoveAction>().Move(mouseGridPosition, ClearBusy);
                        SetBusy();
                    }
                    break;
                }
        }
        
    }

    private void SetBusy()
    {
        isBusy = true;
        onBusyChanged(true);
    }

    private void ClearBusy()
    {
        isBusy = false;
        onBusyChanged(false);
    }

    public void SetSelectedAction(BaseAction selectedAction)
    {
        this.selectedAction = selectedAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction() => selectedAction;
}
