using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float stoppingDistance = 0.1f;
    private Vector3 targetPosition;
    private Animator UnitAnimator;

    private void Awake()
    {
        UnitAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Move(MouseWorld.GetMousePosition());
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

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

}
