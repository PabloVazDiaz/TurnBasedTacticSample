using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    
    [SerializeField] private LayerMask mousePlaneMask;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        

    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, instance.mousePlaneMask);
        return hit.point;
    }
}
