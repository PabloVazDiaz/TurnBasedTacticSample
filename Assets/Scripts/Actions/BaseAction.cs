using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{

    protected Unit unit;
    protected bool isActive;


    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    protected virtual void CompleteAction(Action onActionComplete)
    {
        onActionComplete.Invoke();
    }

    public abstract string GetActionName();
    public abstract void ExecuteAction();

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();
}
