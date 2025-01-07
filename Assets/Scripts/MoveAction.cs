using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    private Vector3 targetPosition;
    private Animator UnitAnimator;
    private Unit unit;

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float stoppingDistance = 0.1f;
    [SerializeField] int  maxMoveDistance = 4;

    private void Awake()
    {
        targetPosition = transform.position;
        UnitAnimator = GetComponentInChildren<Animator>();
        unit = GetComponent<Unit>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
        {
            UnitAnimator.SetBool("IsWalking", false);
            return;
        }


        UnitAnimator.SetBool("IsWalking", true);
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
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
}
