using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;
using Unity.VisualScripting;

[RequireComponent(typeof(Tester))]
public class TesterController : MonoBehaviour
{
    public Tester tester {  get; private set; }
    public Vector2 inputDirection {  get; private set; }
    public Vector2 calculatedDirection { get; private set; }
    public Vector2 gravity { get; private set; }

    [Header("��� ���� �˻�")]
    [SerializeField, Tooltip("����� �� �ִ� �ִ� ��� �����Դϴ�.")]
    float maxSlopeAngle;
    [SerializeField, Tooltip("��縦 üũ�� Raycast �߻� ���� �����Դϴ�.")]
    Transform raycastOrigin;

    private const float RAY_DISTANCE = 2f;
    private RaycastHit slopeHit;
    private bool isOnSlope;

    [Header("�� üũ")]
    [SerializeField, Tooltip("���� �پ� �ִ��� Ȯ���ϱ� ���� CheckBox ���� ����")]
    Transform groundCheck;
    private int groundLayer;
    private bool isGrounded;

    //private void Start()
    //{
    //    tester=GetComponent<Tester>();
    //    groundLayer = 1 << LayerMask.NameToLayer("Ground");
    //}
    //private void Update()
    //{
    //    calculatedDirection = GetDirection(tester.MoveSpeed * MoveState.CONVERT_UNIT_VALUE);
    //    ControlGravity();
    //}

    //public void OnDashInput(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        //���� �߰� ��, ������ȯ��
    //    }
    //}

    //protected Vector3 GetDirection(float currentMoveSpeed)
    //{
    //    isOnSlope = IsOnSlope();
    //    isGrounded = IsGrounded();
    //    Vector3 calculatedDirection =
    //        CalculateNextFrameGroundAngle(currentMoveSpeed) < maxSlopeAngle ?
    //        inputDirection : Vector3.zero;
    //    return calculatedDirection;
    //}
    //protected void ControlGravity()
    //{
    //    gravity = Vector3.down * Mathf.Abs(tester.rigidBody.velocity.y);

    //    if(isGrounded&&isOnSlope)
    //    {
    //        gravity = Vector3.zero;
    //        tester.rigidBody.useGravity = false;
    //        return;
    //    }
    //    tester.rigidBody.useGravity=true;
    //}
    //private float CalculateNextFrameGroundAngle(float moveSpeed)
    //{
    //    Vector3 nextFramePlayerPostition =
    //        raycastOrigin.position + InputDirection * moveSpeed * Time.fixedDeltaTime;

    //    if(Physics.Raycast(nextFramePlayerPostition,Vector3.down,
    //        out RaycastHit hitInfo, RAY_DISTANCE, groundLayer))
    //    {
    //        return Vector3.Angle(Vector3.up, hitInfo.normal);
    //    }
    //    return 0f;
    //}
    //public bool IsGrounded()
    //{
    //    Vector3 boxSize=new Vector3(transform.lossyScale.x,0.4f,transform.lossyScale.z);
    //    return Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, groundLayer);
    //}
    //public bool IsOnSlope()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.down);
    //    if(Physics.Raycast(ray, out slopeHit, RAY_DISTANCE, groundLayer))
    //    {
    //        var angle=Vector3.Angle(Vector3.up, slopeHit.normal);
    //        return angle != 0f && angle < maxSlopeAngle;
    //    }
    //    return false;
    //}
    //public void OnMoveINput(InputAction.CallbackContext context)
    //{
    //    Vector2 input = context.ReadValue<Vector2>();
    //    inputDirection=new Vector3(input.x,0f,input.y);
    //}
    //public void LookAt(Vector3 direction)
    //{
    //    if(direction != Vector3.zero)
    //    {
    //        Quaternion targetAngle=Quaternion.LookRotation(direction);
    //        transform.rotation = targetAngle;
    //    }
    //}
    //protected Vector3 AdjustDirectionToSlope(Vector3 direction)
    //{
    //    Vector3 adjustVelocityDirection =
    //        Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    //    return adjustVelocityDirection;
    //}

}
