using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private BaseAction[] baseActionArray;
    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtSetPosition(gridPosition, this);
    } 

    private void Update()
    {
        
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(gridPosition != newGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public T GetAction<T>() where T:BaseAction => baseActionArray.First(x => x.GetType() == typeof(T)) as T;
    public BaseAction GetAction( BaseAction baseAction) => baseActionArray.First(x => x.GetType() == baseAction.GetType());

    public BaseAction[] GetBaseActionArray() => baseActionArray;
    

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

}
