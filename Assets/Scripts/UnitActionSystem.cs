using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
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
                    if (selectedUnit.GetAction<MoveAction>().IsValidActionGridPosition(mouseGridPosition))
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
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    public void SetSelectedAction(BaseAction selectedAction)
    {
        this.selectedAction = selectedAction;
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
}
