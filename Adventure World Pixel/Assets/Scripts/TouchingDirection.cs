using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection: MonoBehaviour
{
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHit = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHit = new RaycastHit2D[5];
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.06f;
    [SerializeField]
    private bool _IsGrounded = true;
    public bool IsGrounded
    {
        get { return _IsGrounded; }
        set
        {
            _IsGrounded = value;
            animator.SetBool(AnimationString.isGrounded, value);
            
        }
    }
    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value); ;

        }
    }
    [SerializeField]
    private bool _isCeiling;
    private Vector2 wallCHeckDirection => gameObject.transform.localScale.x>0 ? Vector2.right : Vector2.left;

    public bool IsCeiling
    {
        get { return _isCeiling; }
        set
        {
            _isCeiling = value;
            animator.SetBool(AnimationString.isCeiling, value); ;

        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCHeckDirection, castFilter, wallHit, wallDistance) > 0;
        IsCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHit, ceilingDistance) > 0;
    }
}
