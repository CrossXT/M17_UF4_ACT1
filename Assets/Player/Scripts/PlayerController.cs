using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 5f;


    public enum OrientationMode
    {
        ToMovementDirection,
        ToCameraForward,
        ToTarget,
    }
    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.ToMovementDirection;
    [SerializeField] Transform orientationTarget;
    [SerializeField] float angularSpeed = 720f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference Move;
    [SerializeField] InputActionReference Jump;
    [SerializeField] InputActionReference Run;

    CharacterController characterController;


    Camera mainCamera;

    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        Move.action.Enable();
        Jump.action.Enable();
        Run.action.Enable();

        Move.action.performed += OnMove;
        Move.action.started += OnMove;
        Move.action.canceled += OnMove;

        Jump.action.performed += OnJump;
        
    }

    private void Update()
    {
        UpdateMovementOnPlane();
        UpdateVerticalMovement();
        UpdateOrientation();
        //UpdateAnimation();
    }
    Vector3 lastVelocity = Vector3.zero;
    private void UpdateMovementOnPlane()
    {
        Vector3 movement = mainCamera.transform.right * rawMove.x + mainCamera.transform.forward * rawMove.z;


        float oldMovementMagnitude = movement.magnitude;


        Vector3 movementProjectedOnPlane = Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);
        lastVelocity = movementProjectedOnPlane * speed;
    }


    float gravity = -9.8f;
    float verticalVelocity;

    void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        if(characterController.isGrounded)
        {
            verticalVelocity = verticalSpeedOnGrounded;
        }
        if(mustJump)
        {
            mustJump = false;
            if(characterController.isGrounded)
            {
                verticalVelocity = jumpVelocity;
            }
            
        }
    }

    void UpdateOrientation()
    {
        Vector3 desiredDirection = Vector3.forward;
        switch(orientationMode)
        {
            case OrientationMode.ToMovementDirection:
                desiredDirection = lastVelocity;
                break;
            case OrientationMode.ToCameraForward:
                desiredDirection = mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection = orientationTarget.position - transform.position;
                break;
        }
        desiredDirection.y = 0f;

        float angleToApply = angularSpeed * Time.deltaTime;
        //Distancia angular entre transform.forward y desiredDirection

        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
        float realAngleToApply = Mathf.Sign(angularDistance) *            // O vale 1f, o vale -1f
            Math.Min(angleToApply, Mathf.Abs(angularDistance));            // O es lo que me tocaba girar , o es un poco menos porque ha llegado

        Quaternion rotationToApply = Quaternion.AngleAxis(realAngleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;


    }



    //void UpdateAnimation()
    //{
    //    //Esto no hace falta
    //    Vector3 velocidadLocalAMundo = transform.TransformDirection(Vector3.forward);
    //    Vector3 localVelocity = transform.InverseTransformDirection(lastVelocity);

    //    Vector3 normalizedLocalVelocity = localVelocity / speed;

    //    animator.SetFloat("SidewardVelocity", normalizedLocalVelocity.x);
    //    animator.SetFloat("ForwardVelocity", normalizedLocalVelocity.z);
    //}

    private void OnDisable()
    {
        Move.action.Disable();
        Jump.action.Disable();
        Run.action.Disable();

        Move.action.performed -= OnMove;
        Move.action.started -= OnMove;
        Move.action.canceled -= OnMove;

        Jump.action.performed -= OnJump;

        //hurtCollider.onHitRecieved.RemoveListener(OnHitRecieved);
    }

    //private void OnHitRecieved(HitCollider hitCollider, HurtCollider hurtCollider)
    //{
    //    gameObject.SetActive(false);
    //    Invoke(nameof(Resurrect), 3f);
    //}
    //void Resurrect()
    //{
    //    gameObject
    //}

    Vector3 rawMove = Vector3.zero;

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0f, rawInput.y);
    }

    bool mustJump;
    private void OnJump(InputAction.CallbackContext context)
    {
        mustJump = true;
    }

}
