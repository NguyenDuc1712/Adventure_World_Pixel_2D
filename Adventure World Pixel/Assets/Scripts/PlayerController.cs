using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damage))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float jumpPower = 18f;

    Vector2 moveInput;

    TouchingDirection touchingDirection;
    Damage damage1;


    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (IsRuning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {    // dung yen toc do 0
                    return 0;

                }
            }
            else
                // chuyen dong bi khoa khi tan cong
                return 0;

        }
    }
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMove, value);
        }
    }
    [SerializeField]
    private bool _isRuning = false;
    public bool IsRuning
    {
        get
        {
            return _isRuning;
        }
        set
        {
            _isRuning = value;
            animator.SetBool(AnimationString.isRuning, value);
        }
    }
    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        { return _isFacingRight; }

        set
        {
            if (_isFacingRight != value)
            {
                // lat mat cua player ve huong nguoc lai
                transform.localScale *= new Vector2(-1, 1);

            }
            _isFacingRight = value;

        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damage1 = GetComponent<Damage>();
    }

    private void FixedUpdate()
    {
        if (!damage1.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDIrection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDIrection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // mat phai 
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // mat trai
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRuning = true;
        }
        else if (context.canceled)
        {
            IsRuning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationString.Jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            {
                animator.SetTrigger(AnimationString.attackTrigger);

            }

        }
    }

    public void OnHit(int damage, Vector2 knockback)

    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            {
                animator.SetTrigger(AnimationString.bowTrigger);

            }

        }
    }
}
