using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerComtroller : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    float rotationSpeed = 30;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    Vector3 inputVec;
    Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = -(Input.GetAxisRaw("Vertical"));
        inputVec = new Vector3(x, 0, z);
        animator.SetFloat("Input X", z);
        animator.SetFloat("Input X", -(x));
        if (x != 0 || z != 0)
        {
            animator.SetBool("Moving", true);
        }else
        {
            animator.SetBool("Moving", false);
        }
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        UpdateMovement();
    }
    void UpdateMovement()
    {
        Vector3 motion = inputVec;
        motion *= Mathf.Abs((inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1?.7f:1);
        RotateTowardMovement();
        ViewReltativeMovement();
    }
    void RotateTowardMovement()
    {
        if (inputVec != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection),Time.deltaTime * rotationSpeed);
        }
    }
    void ViewReltativeMovement()
    {
        Transform viewtransform = Camera.main.transform;
        Vector3 forward = viewtransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float _vertical = Input.GetAxisRaw("Vertical");
        float _horizontal = Input.GetAxisRaw("Horizontal");
        targetDirection = _horizontal * right + _vertical * forward;
    }
}
