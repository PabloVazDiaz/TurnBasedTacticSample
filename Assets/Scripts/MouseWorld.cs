using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance;
    
    [SerializeField] private LayerMask mousePlaneLayerMask;


    private void Awake()
    {
        Instance = this;
    }
    

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit,float.MaxValue, Instance.mousePlaneLayerMask);
        return hit.point;
    }
}
