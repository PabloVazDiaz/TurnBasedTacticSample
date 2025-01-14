using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    private Vector3 targetPosition;
    private Animator UnitAnimator;
    private Action onMoveCompleted;

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float stoppingDistance = 0.1f;
    [SerializeField] int  maxMoveDistance = 4;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
        UnitAnimator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (!isActive)
            return;

        if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
        {
            UnitAnimator.SetBool("IsWalking", false);
            isActive = false;
            CompleteAction(onMoveCompleted);
            return;
        }


        UnitAnimator.SetBool("IsWalking", true);
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public override void ExecuteAction()
    {
        GridSystemVisual.Instance.UpdateGridVisual();
    }
    public void Move(GridPosition gridPosition, Action onMoveCompleted)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
        this.onMoveCompleted = onMoveCompleted;
        GridSystemVisual.Instance.HideAllGridPosition();
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositions = new();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x < maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z < maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (unitGridPosition == testGridPosition)
                    continue;
                if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) &&
                    !LevelGrid.Instance.HasAnyUnitOnGridPostion(testGridPosition))
                    validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public override string GetActionName()
    {
        return "Move";
    }

    
}
