using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent death;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool immortal = false;


    private float timeHit = 0;
    public float immortaltimer = 0.5f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            if(value == false)
            {
                death .Invoke();
            }

            
        }
    }
    //Vận tốc không nên thay đổi trong khi điều này đúng nhưng cần được các thành phần vật lý khác như => PlayerController

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (immortal)
        {
            if (timeHit > immortaltimer)
            {
                //xoa hieu ung bat tu
                immortal = false;
                timeHit = 0;
            }
            timeHit += Time.deltaTime;
        }

    }

    //trả lại cho dù hàng bị hư hỏng có bị hư hỏng hay không
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !immortal)
        {
            Health -= damage;
            immortal = true;
            //thông báo cho các thành phần đã đăng ký khác rằng thiết bị có thể bị hư hỏng đã bị tấn công để xử lý tình trạng phản hồi và những điều tương tự
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterAction.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        //không thể bị đánh
        return false;
    }
    public void Heal(int healthRectore)
    {
        if(IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth- Health, 0);
            int actuaHeal = Mathf.Min(maxHeal, healthRectore);
            Health += actuaHeal;
            CharacterAction.characterHeal(gameObject, actuaHeal); ;
        }
    }
}
