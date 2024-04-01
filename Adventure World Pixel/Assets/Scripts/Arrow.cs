using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockBack = new  Vector2(0,0);
    public int damage = 10;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, 3f);
        //nếu bạn muốn mũi tên bị ảnh hưởng bởi trọng lực theo mặc định, hãy đặt nó ở chế độ động.
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage1 = collision.GetComponent<Damage>();
        if(damage1 != null)
        {
            // nếu đang hướng mặt về bên trái theo localscale , thì đòn đánh bật lại sẽ lật giá trị của đối phương về b
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            // danh trung muc tieu
            bool gotHit = damage1.Hit(damage, deliveredKnockback);
            if (gotHit)
                Debug.Log(collision.name + " hit for " + damage);
            Destroy(gameObject);
        }
        
    }
}
