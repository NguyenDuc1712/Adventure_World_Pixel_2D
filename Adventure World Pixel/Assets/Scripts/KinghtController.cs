using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damage))]
public class KinghtController : MonoBehaviour
{
    public float walkAcceleration = 5f;
    public float maxspeed = 5f;
    public float stopRate = 0.08f;
    public Zone attackZone;
    public Zone groundZone;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirection touchingDirection;
    Damage damage1;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 WalkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                // huong lat
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                //huon co the di bo
                if (value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    private bool _tarGet = false;


    public bool TarGet
    {
        get
        {
            return _tarGet;
        }
        set
        {
            _tarGet = value;
            animator.SetBool(AnimationString.tarGet, value);
        }
    }
    public bool CanMove

    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationString.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationString.attackCooldown, Math.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damage1 = GetComponent<Damage>();
    }
    private void Update()
    {
        TarGet = attackZone.Collider2D.Count > 0;


        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }


    }
    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }

        if (!damage1.LockVelocity)
        {
            if (CanMove && touchingDirection.IsGrounded)
            {
                // tăng tốc tới tốc độ tối đa
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), -maxspeed, maxspeed), rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
            }
        }

    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Bug");
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void ZoneGround()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}
