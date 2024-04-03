using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeController : MonoBehaviour
{
    public float flySpeed = 3f;
    public float reachedThatpoint = 0.1f;
    public Zone zoneAttack;
    public Collider2D deathColleder;
    public List<Transform> point;
    Animator animator;
    Rigidbody2D rb;
    Damage damage1;
    Transform nextPoint;
    int pointNumer = 0;


    public bool _tarGet = false;

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
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damage1 = GetComponent<Damage>();

    }
    private void Start()
    {
        nextPoint = point[pointNumer];
    }
    void OnEnable()
    {
        damage1.death.AddListener(OnDeath);
    }
    // Update is called once per frame
    void Update()
    {
        TarGet = zoneAttack.Collider2D.Count > 0;
    }
   
    private void FixedUpdate()
    {
        if (damage1.IsAlive)
        {
            if (CanMove)
            {
                Fly();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    private void Fly()
    {
        //bay đến điểm tiếp theo
        Vector2 directionTopoint = (nextPoint.position - transform.position).normalized;
        //kiểm tra xem chúng ta đã đến điểm chưa
        float distance = Vector2.Distance(nextPoint.position, transform.position);
        rb.velocity = directionTopoint * flySpeed;
        FlightDirection();
        //xem liệu chúng ta có cần đổi điểm không
        if (distance <= reachedThatpoint)
        {
            //chuyển đến điểm tiếp theo
            pointNumer++;
            if (pointNumer >= point.Count)
            {
                // troử lại điểm ban đầu
                pointNumer = 0;
            }
            // Hình thức chuyển đổi điểm 
            // điểm tiếp theo sẽ bằng điểm tại số điểm 
            nextPoint = point[pointNumer];


        }
    }

    private void FlightDirection()

    {
        Vector3 localScale= transform.localScale;
        if (transform.localScale.x > 0)
        {
            // hướng về bên phải
            if(rb.velocity.x<0)
            {
                // lật
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
            
        }
        else
        {
            // hướng về bên trai
            if(rb.velocity.x>0)
            {
                //lật
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }

    public void OnDeath()
    {          
            //dead flyer falls to the ground
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
        deathColleder.enabled = true;
    }
    public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
} 
