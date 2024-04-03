using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockBack = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiem tra xem co the tan cong khong 
        Damage damage = collision.GetComponent<Damage>();
        if (damage != null)
        {
            // nếu đang hướng mặt về bên trái theo localscale , thì đòn đánh bật lại sẽ lật giá trị của đối phương về b
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            // danh trung muc tieu
            bool gotHit = damage.Hit(attackDamage, deliveredKnockback);
            if(gotHit)
            {
                Debug.Log(collision.name + " Hit For " + attackDamage);
                    
            }
        }
    }
}
